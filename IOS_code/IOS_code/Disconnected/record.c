/*make a avi movi by licl*/

#include <sys/types.h>
#include <unistd.h>
#include <sys/stat.h>
#include <fcntl.h>
#include <stdio.h>
#include <semaphore.h>
#include <pthread.h>
#include <stdlib.h>

#include <errno.h> 
#include <string.h> 
#include <netdb.h> 
#include <sys/types.h> 
#include <netinet/in.h> 
#include <sys/socket.h> 

#include "avi.h"


#define RECORD_TIMES 60*10

char buffer[1024*60];
char name[500];
char path[500];
char driverecord=0;

volatile int write_avi_action;

int to_framebuffer(unsigned char *framedata, size_t size);
int make_avi_init(void);
int make_avi(char *filename, video_profile *video_config, int frame_count, void *(*start_routine)(void *));



int abcmain(char *arg)
{ 
     int sockfd; 
     int len,totlen=0;
     struct sockaddr_in server_addr;
     const char GET[]={"GET /?action=stream HTTP/1.0\r\n\r\n"}; 
     const char END[]={"\r\n--boundarydonotcross\r\n"};
     const char RESP[]={"Content-Type: image/jpeg\r\nContent-Length: "};

     char *pchr;
    
     int gapindex=0;
     

     char grasp=0;
     int frame_len=0;
     video_profile avi_cfg;
    strcpy(path,arg);
    while(1){
        
        if(driverecord==0){
            sleep(1);
            continue;
        }
        /* øÕªß≥Ã–Úø™ ºΩ®¡¢ sockfd√Ë ˆ∑˚ */
        if((sockfd=socket(AF_INET,SOCK_STREAM,0))==-1) // AF_INET:Internet;SOCK_STREAM:TCP
        {
            fprintf(stderr,"Socket Error:%s\a\n",strerror(errno));
            continue;
        }
        
        /* øÕªß≥Ã–ÚÃÓ≥‰∑˛ŒÒ∂Àµƒ◊ ¡œ */
        bzero(&server_addr,sizeof(server_addr)); // ≥ı ºªØ,÷√0
        server_addr.sin_family=AF_INET;          // IPV4
        server_addr.sin_port=htons(8080);  // (Ω´±æª˙∆˜…œµƒshort ˝æ›◊™ªØŒ™Õ¯¬Á…œµƒshort ˝æ›)∂Àø⁄∫≈
        //server_addr.sin_addr=*((struct in_addr *)host->h_addr); // IPµÿ÷∑
        inet_pton(AF_INET,"192.168.10.1",&server_addr.sin_addr);
         /* øÕªß≥Ã–Ú∑¢∆¡¨Ω”«Î«Û */
         if(connect(sockfd,(struct sockaddr *)(&server_addr),sizeof(struct sockaddr))==-1) { 
             fprintf(stderr,"Connect Error:%s\a\n",strerror(errno)); 
             sleep(1);
             close(sockfd);
             continue;
         } 
         avi_cfg.width=640;   //length per dma buffer
         avi_cfg.height=480;
         avi_cfg.framerate=20;
         avi_cfg.gop_size=1;
         avi_cfg.qmax=30;
         avi_cfg.qmin=30;   
         avi_cfg.quant=1;
        
         sprintf(name,"%s/video%d.avi",path,gapindex++);
         if(gapindex==5)
            gapindex=0;
        
         make_avi_init();
         make_avi(name,&avi_cfg,RECORD_TIMES, NULL);
         //printf("file name:%s",name);
            
         write(sockfd,GET,strlen(GET));

         grasp=0;
         frame_len=0;
         totlen=0;
        
        while(1){
            
            if(driverecord){
                 if(totlen>50*1024){
                    totlen=0;
                    grasp=0;
                }
                len=read(sockfd,buffer+totlen,10*1024);
                if(len>0){
                    if(grasp){//grasping
                        buffer[totlen+len]='\0';
                        pchr=strstr(buffer+totlen,END);
                        if(pchr){
                            grasp=0;
                            to_framebuffer(buffer,pchr-buffer);
                           // printf("to buf!\n");
                        }else{
                            totlen+=len;//save
                        }
                        
                    }
                    if(!grasp){//≤È’“÷°Õ∑≤ø
                        buffer[totlen+len]='\0';
                        pchr=strstr(buffer+totlen,RESP);//≤È’“◊ ‘¥Õ∑≤ø,Àµ√˜…œ“ª÷°“—æ≠Ω· ¯
                        if(pchr){//πÿº¸÷°
                            //∑¢œ÷πÿº¸÷°∫Û£¨÷ÿ–¬ø™ º¥Ê¥¢
                            pchr=strstr(pchr,"\r\n\r\n");			
                            if(pchr){
                                grasp=1;
                                pchr+=4;//skip "\r\n\n"
                                totlen=len-(pchr-(buffer+totlen));//÷°Õ∑ ◊≤ø≥§∂»
                                memcpy(buffer,pchr,totlen);//÷ÿ–¬◊ÈΩ®÷°
                            }
                        }
                    }
        
                }else{//error
                    driverecord=0;
                    close(sockfd);
                    signal_all();
                    while (write_avi_action);
                    break;
                }
            }else{
                close(sockfd);
                signal_all();
                while (write_avi_action);
                break;
            }
            
            if((!write_avi_action)&&driverecord){
                
                sprintf(name,"%s/video%d.avi",path,gapindex++);
                if(gapindex==5)
                    gapindex=0;
                make_avi_init();
                make_avi(name,&avi_cfg,RECORD_TIMES, NULL);
            }
        }
    

    }
 
 }

