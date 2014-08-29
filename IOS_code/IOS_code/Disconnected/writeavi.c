#include <sys/types.h>
#include <unistd.h>
#include <sys/stat.h>
#include <fcntl.h>
#include <stdio.h>
#include <semaphore.h>
#include <pthread.h>
#include <stdlib.h>

#include "avi.h"

//#define _DEBUG_
#ifdef _DEBUG_
	#define dbprf(args...) printf(args)
#else
	#define dbprf(args...)
#endif

static volatile int fb_current = 0;
static FRAME_BUFFER fb[FRAME_BUFFER_MAX];

volatile int write_avi_action = 0;

static int avi_header_init(AVI_HEADER *avi_header, AVI_CONFIG *avi_cfg);
static int _fwrite(void *ptr, size_t size, size_t nmemb, FILE *stream);
static int next_fb(int frame);
static int pre_fb(int frame);
static int write_avi_init(AVI_CONFIG *avi_cfg, AVI_HEADER *avi_header, AVI_INDEX *avi_index);
static void *write_avi(void *arg);
static int write_avi_movi(AVI_CONFIG *avi_cfg, AVI_HEADER *avi_header, AVI_INDEX *avi_index);
static int write_avi_index(AVI_CONFIG *avi_cfg, AVI_HEADER *avi_header, AVI_INDEX *avi_index);
static void write_avi_close(AVI_INDEX *avi_index, AVI_CONFIG *avi_cfg, int tag);
static int write_avi_header(AVI_CONFIG *avi_cfg, AVI_HEADER *avi_header);



sem_t * CreateSemaphore( const char * inName, const int inStartingCount )
{
    sem_t * semaphore = sem_open( inName, O_CREAT, 0644, inStartingCount );
    return semaphore;
}


//
char DestroySemaphore( sem_t * inSemaphore )
{
    int retErr = sem_close( inSemaphore );
    
    //
    if( retErr == -1 )
    {
      
        return 0;
    }
    
    //
    return 1;
}

//
void SignalSemaphore( sem_t * inSemaphore )
{
    sem_post( inSemaphore );
}

//
void WaitSemaphore( sem_t * inSemaphore )
{
    sem_wait( inSemaphore );
}


char ClearSemaphore( const char * inName)
{
    int retErr = sem_unlink(inName);
    
    if (retErr == -1) {
        
        return 0;
        
    }
    
    return 1;
}


int make_avi_init(void)
{
	int int_buf, res;
	
	for(int_buf=0; int_buf<FRAME_BUFFER_MAX; int_buf++)
	{
        char *semname[20];
        sprintf(semname,"avisem%d",int_buf);
		fb[int_buf].sem_lock= CreateSemaphore(semname, 0);
		if(!fb[int_buf].sem_lock)
		{
			dbprf("Semaphore initialization failed!\n");
			return -1;
		}
		
		fb[int_buf].blk.fcc[0] = '0';
		fb[int_buf].blk.fcc[1] = '0';
		fb[int_buf].blk.fcc[2] = 'd';
		fb[int_buf].blk.fcc[3] = 'c';
		
		fb[int_buf].blk.size = 0;
		//sem_post(&fb[int_buf].sem_lock);
        SignalSemaphore(fb[int_buf].sem_lock);
        
        
    }
	
	sem_wait(fb[0].sem_lock);
	fb_current=0;

	return 0;
}


int signal_all(void)
{
	int int_buf;
	
	for(int_buf=0; int_buf<FRAME_BUFFER_MAX; int_buf++)
	{
         SignalSemaphore(fb[int_buf].sem_lock);
        
        
    }
	return 0;
}




void make_avi_close(void)
{
	int i;
	char *semname[20];
	for(i=0;i<FRAME_BUFFER_MAX;i++){
		//sem_destroy(fb[i].sem_lock);
        
        sprintf(semname,"avisem%d",i);
        DestroySemaphore(fb[i].sem_lock);
        ClearSemaphore(semname);
    }
    
    
	return;
}





static int avi_header_init(AVI_HEADER *avi_header, AVI_CONFIG *avi_cfg)
{
//	fd = fopen(filename, "wb");
	
	avi_header->fcc[0] = 'R';
	avi_header->fcc[1] = 'I';
	avi_header->fcc[2] = 'F';
	avi_header->fcc[3] = 'F';
	dbprf("%c\n",avi_header->fcc[3]);
	avi_header->filetype[0] = 'A';
	avi_header->filetype[1] = 'V';
	avi_header->filetype[2] = 'I';
	avi_header->filetype[3] = ' ';
	
	avi_header->fcc_list_p[0] = 'L';
	avi_header->fcc_list_p[1] = 'I';
	avi_header->fcc_list_p[2] = 'S';
	avi_header->fcc_list_p[3] = 'T';
	avi_header->list_p_size = 4 + sizeof(AVI_AVIH) + 4 + 4 + sizeof(AVI_STRL_VIDEO);    // + sizeof(AVI_STRL_AUDIO);
	
	avi_header->fcc_hdrl[0] = 'h';
	avi_header->fcc_hdrl[1] = 'd';
	avi_header->fcc_hdrl[2] = 'r';
	avi_header->fcc_hdrl[3] = 'l';
	
	avi_header->mainheader.fcc[0] = 'a';
	avi_header->mainheader.fcc[1] = 'v';
	avi_header->mainheader.fcc[2] = 'i';
	avi_header->mainheader.fcc[3] = 'h';
	avi_header->mainheader.cb = sizeof(AVI_AVIH) - 8;
	avi_header->mainheader.dwMicroSecPerFrame =50000;// 0x0F4240;
	avi_header->mainheader.dwMaxBytesPerSec = 1228800;
	avi_header->mainheader.dwFlags = 0x0810;
	avi_header->mainheader.dwTotalFrames = avi_cfg->write_frame_count;
	avi_header->mainheader.dwStreams = 0x01;
	avi_header->mainheader.dwWidth = avi_cfg->width;
	avi_header->mainheader.dwHeight = avi_cfg->height;
	
	
	avi_header->fcc_list_v[0] = 'L';
	avi_header->fcc_list_v[1] = 'I';
	avi_header->fcc_list_v[2] = 'S';
	avi_header->fcc_list_v[3] = 'T';
	avi_header->list_v_size = sizeof(AVI_STRL_VIDEO);   // + sizeof(AVI_STRL_audio);
	
	avi_header->video_info.fcc_strl[0] = 's';
	avi_header->video_info.fcc_strl[1] = 't';
	avi_header->video_info.fcc_strl[2] = 'r';
	avi_header->video_info.fcc_strl[3] = 'l';
	
	avi_header->video_info.avi_stream.fcc[0] = 's';
	avi_header->video_info.avi_stream.fcc[1] = 't';
	avi_header->video_info.avi_stream.fcc[2] = 'r';
	avi_header->video_info.avi_stream.fcc[3] = 'h';
	avi_header->video_info.avi_stream.cb = sizeof(AVI_STRH) - 8;
	avi_header->video_info.avi_stream.fccType[0] = 'v';
	avi_header->video_info.avi_stream.fccType[1] = 'i';
	avi_header->video_info.avi_stream.fccType[2] = 'd';
	avi_header->video_info.avi_stream.fccType[3] = 's';
	avi_header->video_info.avi_stream.fccHandler[0] = 'M';
	avi_header->video_info.avi_stream.fccHandler[1] = 'J';
	avi_header->video_info.avi_stream.fccHandler[2] = 'P';
	avi_header->video_info.avi_stream.fccHandler[3] = 'G';
//	avi_header->video_info.avi_stream.dwInitialFrames = 0x01;
	avi_header->video_info.avi_stream.dwScale = avi_cfg->gop;
	avi_header->video_info.avi_stream.dwRate = avi_cfg->framerate;
	avi_header->video_info.avi_stream.dwLength = avi_cfg->write_frame_count;
	avi_header->video_info.avi_stream.dwQuality = 0xFFFFFFFF;
	avi_header->video_info.avi_stream.rcFrame.right = avi_cfg->width;
	avi_header->video_info.avi_stream.rcFrame.bottom = avi_cfg->height;
	
	avi_header->video_info.stream_format.fcc[0] = 's';
	avi_header->video_info.stream_format.fcc[1] = 't';
	avi_header->video_info.stream_format.fcc[2] = 'r';
	avi_header->video_info.stream_format.fcc[3] = 'f';
	avi_header->video_info.stream_format.cb = sizeof(TBitmapInfoHeader) - 8;
	avi_header->video_info.stream_format.biSize = sizeof(TBitmapInfoHeader) - 8;
	avi_header->video_info.stream_format.biWidth = avi_cfg->width;
	avi_header->video_info.stream_format.biHeight = avi_cfg->height;
	avi_header->video_info.stream_format.biPlanes = 0x01;
	avi_header->video_info.stream_format.biBitCount = 0x18;
	avi_header->video_info.stream_format.biCompression[0] = 'M';
	avi_header->video_info.stream_format.biCompression[1] = 'J';
	avi_header->video_info.stream_format.biCompression[2] = 'P';
	avi_header->video_info.stream_format.biCompression[3] = 'G';
	avi_header->video_info.stream_format.biSizeImage = 921600;
	
/*	avi_header->fcc_list_a[0] = 'L';
	avi_header->fcc_list_a[1] = 'I';
	avi_header->fcc_list_a[2] = 'S';
	avi_header->fcc_list_a[3] = 'T';
	avi_header->list_a_size = sizeof(AVI_STRL_AUDIO);
	
	avi_header->audio_info.fcc_strl[0] = 's';
	avi_header->audio_info.fcc_strl[1] = 't';
	avi_header->audio_info.fcc_strl[2] = 'r';
	avi_header->audio_info.fcc_strl[3] = 'l';
	
	avi_header->audio_info.avi_stream.fcc[0] = 's';
	avi_header->audio_info.avi_stream.fcc[1] = 't';
	avi_header->audio_info.avi_stream.fcc[2] = 'r';
	avi_header->audio_info.avi_stream.fcc[3] = 'h';
	avi_header->audio_info.avi_stream.cb = sizeof(AVI_STRH) - 8;
	avi_header->audio_info.avi_stream.fccType[0] = 'a';
	avi_header->audio_info.avi_stream.fccType[1] = 'u';
	avi_header->audio_info.avi_stream.fccType[2] = 'd';
	avi_header->audio_info.avi_stream.fccType[3] = 's';
	avi_header->audio_info.avi_stream.fccHandler[0] = 0x01;
	avi_header->audio_info.avi_stream.dwInitialFrames = 0x01;
	
	avi_header->audio_info.stream_format.fcc[0] = 's';
	avi_header->audio_info.stream_format.fcc[1] = 't';
	avi_header->audio_info.stream_format.fcc[2] = 'r';
	avi_header->audio_info.stream_format.fcc[3] = 'f';
	avi_header->audio_info.stream_format.cb = sizeof(TWaveFormatEx) - 8;*/
	
	avi_header->fcc_list_m[0] = 'L';
	avi_header->fcc_list_m[1] = 'I';
	avi_header->fcc_list_m[2] = 'S';
	avi_header->fcc_list_m[3] = 'T';
	
	avi_header->fcc_movi[0] = 'm';
	avi_header->fcc_movi[1] = 'o';
	avi_header->fcc_movi[2] = 'v';
	avi_header->fcc_movi[3] = 'i';
//	_fwrite(avi_header, sizeof(AVI_HEADER), 1, fd);
//	fclose(fd);
	
	return 0;
}

static int _fwrite(void *ptr, size_t size, size_t nmemb, FILE *stream)
{
	size_t s;
	
	if(NULL==stream)
		return -1;
	
	if(NULL==ptr || 0==size || 0==nmemb)
		return 0;
	dbprf("write file:%dbyte!\n",size);
	s = fwrite(ptr, size, nmemb, stream);
	if(0==s || s<nmemb)
	{
		dbprf("write avi failed!%d\n",s);
		return -1;
	}
	dbprf("write succeed!\n");
	return 0;
}

static int next_fb(int frame)
{
	int intbuf = frame;
	
	intbuf++;
	if(intbuf>=FRAME_BUFFER_MAX)
		intbuf = 0;
	return intbuf;
}

static int pre_fb(int frame)
{
	int intbuf = frame;
	
	intbuf--;
	if(intbuf<0)
		intbuf = FRAME_BUFFER_MAX - 1;
	return intbuf;
}

int to_framebuffer(unsigned char *framedata, size_t size)
{
	int fb_c;
	
	if(NULL==framedata || size<=0 || size>(FRAME_BUFFER_SIZE-1))
		return 0;
	
	/*if(0!=framedata[3])
		return 0;*/
	
	fb_c = next_fb(fb_current);
	
	sem_wait(fb[fb_c].sem_lock);
		
	memcpy(fb[fb_c].blk.fb, framedata, size);
	fb[fb_c].blk.size = (dword)size;
	
	sem_post(fb[fb_current].sem_lock);
	
	fb_current = fb_c;
	dbprf("fb[%d],size:%d\n",fb_c, fb[fb_c].blk.size);
	return 0;
}

static int write_avi_init(AVI_CONFIG *avi_cfg, AVI_HEADER *avi_header, AVI_INDEX *avi_index)
{
	int intbuf;
	
	dbprf("avi_header_init\n");
	memset(avi_header, 0, sizeof(AVI_HEADER));
	avi_header_init(avi_header, avi_cfg);
	
	dbprf("set value\n");
	for(intbuf=0; intbuf<avi_cfg->write_frame_count; intbuf++)
	{
		(avi_index+intbuf)->dwChunkId = 0x63643030;   //'00bc'
		(avi_index+intbuf)->dwFlags = 0x00000010;
	}
	dbprf("fopen file\n");
	avi_cfg->fd = fopen(avi_cfg->filename, "wb");
	if(NULL==avi_cfg->fd)
	{
		dbprf("open file failed!\n");
		return -1;
	}
	dbprf("fwrite avi_header\n");
	if(-1==_fwrite(avi_header, sizeof(AVI_HEADER), 1, avi_cfg->fd))
	{
		dbprf("write file failed!\n");
		return -1;
	}
	
	avi_header->list_m_size = 4;
	
	return 0;
}

int make_avi(char *filename, video_profile *video_config, int frame_count, void *(*start_routine)(void *))
{
	int res;
	pthread_t p;
	AVI_CONFIG *avi_cfg;
	
	if(write_avi_action)
	{
		dbprf("writing avi!\n");
		return -1;
	}
	write_avi_action = 1;
	
	avi_cfg = (AVI_CONFIG *)calloc(1, sizeof(AVI_CONFIG));
	if(NULL==avi_cfg)
	{
		dbprf("required memory for avi_cfg failed!\n");
		write_avi_action = 0;
		return -1;
	}
	
	strcpy(avi_cfg->filename ,filename);
	dbprf("filename:%s\n",avi_cfg->filename);
	avi_cfg->width = video_config->width;
	dbprf("width:%d\n",avi_cfg->width);
	avi_cfg->height = video_config->height;
	dbprf("height:%d\n",avi_cfg->height);
	avi_cfg->framerate = video_config->framerate;
	dbprf("framerate:%d\n",avi_cfg->framerate);
	avi_cfg->gop = video_config->gop_size;
	dbprf("gop:%d\n",avi_cfg->gop);
	avi_cfg->write_frame_count = frame_count*avi_cfg->framerate/avi_cfg->gop;
	dbprf("write_frame_count:%d\n",avi_cfg->write_frame_count);
	avi_cfg->frame_max_size = 0;
	//avi_cfg->start_routine = start_routine;
	
	res = pthread_create(&p, NULL, write_avi, (void *)avi_cfg);
	if(res)
	{
		dbprf("thread create failed!\n");
		write_avi_action = 0;
		return -1;
	}
	
	return 0;
}

static void *write_avi(void *arg)
{
	AVI_HEADER avi_header;
	AVI_INDEX *avi_index = NULL;
	AVI_CONFIG *avi_cfg = (AVI_CONFIG *)arg;
	
	dbprf("calloc AVI_INDEX size=%d\n",sizeof(AVI_INDEX)*avi_cfg->write_frame_count);
	avi_index = (AVI_INDEX *)calloc(1, sizeof(AVI_INDEX)*avi_cfg->write_frame_count);
	if(NULL==avi_index)
	{
		dbprf("required memory failed!\n");
		free(avi_cfg);
		return NULL;
	}
	
	dbprf("write_avi_init\n");
	if(-1==write_avi_init(avi_cfg, &avi_header, avi_index))
	{
		write_avi_close(avi_index, avi_cfg, 0);
		return NULL;
	}
	dbprf("write_avi_movi\n");
	if(-1==write_avi_movi(avi_cfg, &avi_header, avi_index))
	{
		write_avi_close(avi_index, avi_cfg, 0);
		return NULL;
	}
	dbprf("write_avi_index\n");
	if(-1==write_avi_index(avi_cfg, &avi_header, avi_index))
	{
		write_avi_close(avi_index, avi_cfg, 0);
		return NULL;
	}
	dbprf("write_avi_header\n");
	if(-1==write_avi_header(avi_cfg, &avi_header))
	{
		write_avi_close(avi_index, avi_cfg, 0);
		return NULL;
	}
	printf("avi finish\n");
	write_avi_close(avi_index, avi_cfg, 1);
	return NULL;
}


extern char driverecord;//broken the record

static int write_avi_movi(AVI_CONFIG *avi_cfg, AVI_HEADER *avi_header, AVI_INDEX *avi_index)
{
	int int_buf = 0;
	int fb_c;
	size_t sizebuf = 0;
	size_t index_offset = 4;
	
	fb_c = next_fb(next_fb(next_fb(fb_current)));
	
	while((int_buf<avi_cfg->write_frame_count)&&(driverecord))
	{
		dbprf("int_buf:%d,write_frame_count:%d\n", int_buf, avi_cfg->write_frame_count);
		if(0==fb[fb_c].blk.size)
		{
			fb_c = next_fb(fb_c);
			usleep(1);
			continue;
		}
		
		sem_wait(fb[fb_c].sem_lock);
		
		if(0==fb[fb_c].blk.size%2)
			sizebuf = fb[fb_c].blk.size + 8;
		else
		{
			fb[fb_c].blk.fb[fb[fb_c].blk.size] = 0x00;
			sizebuf = fb[fb_c].blk.size + 9;
		}
		
		dbprf("write fb[%d]\n",fb_c);
		if(-1==_fwrite(&fb[fb_c].blk, sizebuf, 1, avi_cfg->fd))
		{
			dbprf("write movi failed!\n");
			sem_post(fb[fb_c].sem_lock);
			return -1;
		}
		
		avi_header->list_m_size += sizebuf;

		(avi_index+int_buf)->dwOffset = index_offset;
		index_offset += sizebuf;
		(avi_index+int_buf)->dwSize = fb[fb_c].blk.size;
		
		if(avi_cfg->frame_max_size<fb[fb_c].blk.size)
			avi_cfg->frame_max_size = fb[fb_c].blk.size;
		fb[fb_c].blk.size=0;
		sem_post(fb[fb_c].sem_lock);
		
		fb_c = next_fb(fb_c);
		int_buf++;
	}
    avi_cfg->write_frame_count=int_buf;
	
	return 0;
}

static int write_avi_index(AVI_CONFIG *avi_cfg, AVI_HEADER *avi_header, AVI_INDEX *avi_index)
{
	dword dwbuf;
	size_t sizebuf =  sizeof(AVI_INDEX) * avi_cfg->write_frame_count;
	
	dwbuf = 0x31786469;    //'idx1'
	if(-1==_fwrite(&dwbuf, sizeof(dword), 1, avi_cfg->fd))
	{
		dbprf("write 'idx1' failed!\n");
		return -1;
	}
	
	dwbuf = sizebuf;
	if(-1==_fwrite(&dwbuf, sizeof(dword), 1, avi_cfg->fd))
	{
		dbprf("write index size failed!\n");
		return -1;
	}
	
	if(-1==_fwrite(avi_index, sizebuf, 1, avi_cfg->fd))
	{
		dbprf("write index failed!\n");
		return -1;
	}
	
	avi_header->dwavisize += (sizebuf + 8);
	
	return 0;
}

static void write_avi_close(AVI_INDEX *avi_index, AVI_CONFIG *avi_cfg, int tag)
{
	dbprf("close fd\n");
	if(NULL!=avi_cfg->fd)
	{
		fclose(avi_cfg->fd);
		avi_cfg->fd = NULL;
	}
	dbprf("free avi_index\n");
	if(NULL!=avi_index)
	{
		free(avi_index);
		avi_index = NULL;
	}
    
    make_avi_close();
    
	dbprf("run callback function!\n");
	/*if(tag && NULL!=avi_cfg->start_routine)
		avi_cfg->start_routine((void *)avi_cfg->filename);
	dbprf("free avi_cfg\n");
	if(NULL!=avi_cfg)
	{
		free(avi_cfg);
		avi_cfg = NULL;
	}*/
	write_avi_action = 0;
	dbprf("all done,return\n");
    
	return ;
}

static int write_avi_header(AVI_CONFIG *avi_cfg, AVI_HEADER *avi_header)
{
    avi_header_init(avi_header,avi_cfg);
	avi_header->dwavisize += avi_header->list_m_size + sizeof(AVI_HEADER) - 12;
	avi_header->mainheader.dwSuggestedBufferSize = avi_cfg->frame_max_size;
//	avi_header->video_info.avi_stream.dwScale = 0x01;
	avi_header->video_info.avi_stream.dwSuggestedBufferSize = avi_cfg->frame_max_size;
	
	fseek(avi_cfg->fd, 0, SEEK_SET);
	
	dbprf("rewrite avi_header\n");
	if(-1==_fwrite(avi_header, sizeof(AVI_HEADER), 1, avi_cfg->fd))
	{
		dbprf("rewrite header failed!\n");
		return -1;
	}
	
	return 0;
}


