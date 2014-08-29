#ifndef _AVI_H
#define _AVI_H

typedef unsigned char	byte;	/* 8-bit */
typedef unsigned short	word;	/* 16-bit */
typedef unsigned int	dword;	/* 32-bit */

#define FRAME_BUFFER_MAX 10
#define FRAME_BUFFER_SIZE 1024*60

typedef struct _avi_avih {
	byte fcc[4];   // 必须为‘avih’
	dword  cb;    // 本数据结构的大小，不包括最初的8个字节（fcc和cb两个域）
	dword  dwMicroSecPerFrame;   // 视频帧间隔时间（以毫秒为单位）
	dword  dwMaxBytesPerSec;     // 这个AVI文件的最大数据率
	dword  dwPaddingGranularity; // 数据填充的粒度
	dword  dwFlags;         // AVI文件的全局标记，比如是否含有索引块等
	dword  dwTotalFrames;   // 总帧数
	dword  dwInitialFrames; // 为交互格式指定初始帧数（非交互格式应该指定为0）
	dword  dwStreams;       // 本文件包含的流的个数
	dword  dwSuggestedBufferSize; // 建议读取本文件的缓存大小（应能容纳最大的块）
	dword  dwWidth;         // 视频图像的宽（以像素为单位）
	dword  dwHeight;        // 视频图像的高（以像素为单位）
	dword  dwReserved[4];   // 保留
} AVI_AVIH;

typedef struct _avi_strh {
	byte fcc[4];  // 必须为‘strh’
	dword  cb;   // 本数据结构的大小，不包括最初的8个字节（fcc和cb两个域）
	byte fccType[4];    // 流的类型：‘auds’（音频流）、‘vids’（视频流）、
	              //‘mids’（MIDI流）、‘txts’（文字流）
	byte fccHandler[4]; // 指定流的处理者，对于音视频来说就是解码器
	dword  dwFlags;    // 标记：是否允许这个流输出？调色板是否变化？
	word   wPriority;  // 流的优先级（当有多个相同类型的流时优先级最高的为默认流）
	word   wLanguage;
	dword  dwInitialFrames; // 为交互格式指定初始帧数
	dword  dwScale;   // 这个流使用的时间尺度
	dword  dwRate;
	dword  dwStart;   // 流的开始时间
	dword  dwLength;  // 流的长度（单位与dwScale和dwRate的定义有关）
	dword  dwSuggestedBufferSize; // 读取这个流数据建议使用的缓存大小
	dword  dwQuality;    // 流数据的质量指标（0 ~ 10,000）
	dword  dwSampleSize; // Sample的大小
	struct {
		short int left;
		short int top;
		short int right;
		short int bottom;
	} rcFrame;  // 指定这个流（视频流或文字流）在视频主窗口中的显示位置
             // 视频主窗口由AVIMAINHEADER结构中的dwWidth和dwHeight决定
} AVI_STRH;

#pragma pack(2)
typedef struct _TBitmapInfoHeader {
	byte        fcc[4];     //'strf'
	dword       cb;
	dword       biSize;
	dword       biWidth;
	dword       biHeight;
	word        biPlanes;
	word        biBitCount;
	byte        biCompression[4];
	dword       biSizeImage;
	dword       biXPelsPerMeter;
	dword       biYPelsPerMeter;
	dword       biClrUsed;
	dword       biClrImportant;
}TBitmapInfoHeader;
#pragma option pop

#pragma pack(2)
typedef struct _TWaveFormatEx {
	byte        fcc[4];     //'strf'
	dword       cb;
	word        wFormatTag;         // format type
	word        nChannels;          // number of channels (i.e. mono, stereo...) 
	dword       nSamplesPerSec;     // sample rate 
	dword       nAvgBytesPerSec;    // for buffer estimation 
	word        nBlockAlign;        // block size of data 
	word        wBitsPerSample;     // number of bits per sample of mono data 
	dword        cbSize;             // the count in bytes of the size of 
} TWaveFormatEx;
#pragma option pop

typedef struct _avi_strl_video {
	byte               fcc_strl[4];      //必须为'strl'
	AVI_STRH           avi_stream;
	TBitmapInfoHeader  stream_format;
} AVI_STRL_VIDEO;

typedef struct _avi_strl_audio {
	byte           fcc_strl[4];      //必须为'strl'
	AVI_STRH       avi_stream;
	TWaveFormatEx  stream_format;
} AVI_STRL_AUDIO;



typedef struct _avi_header {
	byte   fcc[4];    //必须为'RIFF'
	dword  dwavisize;    //avi数据大小
	byte   filetype[4];   //文件类型
	
	byte            fcc_list_p[4];    //'LIST'
	dword           list_p_size;      // 4 + sizeof(AVI_AVIH) + 4 + 4 + sizeof(AVI_STRL_VIDEO) + sizeof(AVI_STRL_audio)
	byte            fcc_hdrl[4];      //'hdrl'
	AVI_AVIH        mainheader;
	
	byte            fcc_list_v[4];    //'LIST'
	dword           list_v_size;      // sizeof(AVI_STRL_VIDEO)
	AVI_STRL_VIDEO  video_info;
//	byte            fcc_list_a[4];    //'LIST'
//	dword           list_a_size;      // sizeof(AVI_STRL_AUDIO)
//	AVI_STRL_AUDIO  audio_info;
	
	byte            fcc_list_m[4];    //'LIST'
	dword           list_m_size;
	byte            fcc_movi[4];      //'movi'
} AVI_HEADER;

typedef struct _avioldindex_entry {
	dword   dwChunkId;   // 表征本数据块的四字符码
	dword   dwFlags;     // 说明本数据块是不是关键帧、是不是‘rec ’列表等信息
	dword   dwOffset;    // 本数据块在文件中的偏移量
	dword   dwSize;      // 本数据块的大小
} AVI_INDEX;

typedef struct _frame_block {
	byte fcc[4];
	dword size;
	unsigned char fb[FRAME_BUFFER_SIZE];
} FRAME_BLOCK;

typedef struct _frame_buffer {
	sem_t *sem_lock;
	FRAME_BLOCK blk;
} FRAME_BUFFER;

typedef struct _video_config {
	char filename[512];
	unsigned int width;
	unsigned int height;
	unsigned int framerate;
	unsigned int gop;
//	dword IFrameInterval;
	unsigned int write_frame_count;
	FILE *fd;
	size_t frame_max_size;
	void *(*start_routine)(void *);
} AVI_CONFIG;

typedef struct video_profile
{
    unsigned int bit_rate;
    unsigned int width;   //length per dma buffer
    unsigned int height;
    unsigned int framerate;
    unsigned int frame_rate_base;
    unsigned int gop_size;
    unsigned int qmax;
    unsigned int qmin;   
    unsigned int quant;
//++ Foster
//    unsigned int enable_4mv;
//-- Foster
    void *coded_frame;
}video_profile;

#endif
