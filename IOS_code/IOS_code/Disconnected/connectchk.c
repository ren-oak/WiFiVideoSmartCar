//
//  File.c
//  smart car
//
//  Created by apple on 13-5-29.
//  Copyright (c) 2013年 John E. Ray. All rights reserved.
//

#include <stdio.h>
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



int connect_check(int port)
{
    int sockfd;
    int timeout=0;
    struct sockaddr_in server_addr;
    
    while(timeout++<50){
        
        if((sockfd=socket(AF_INET,SOCK_STREAM,0))==-1) // AF_INET:Internet;SOCK_STREAM:TCP
        {
            fprintf(stderr,"Socket Error:%s\a\n",strerror(errno));
            return -1;
        }
        
        bzero(&server_addr,sizeof(server_addr));
        server_addr.sin_family=AF_INET;          // IPV4
        server_addr.sin_port=htons(port);  
        
        inet_pton(AF_INET,"192.168.10.1",&server_addr.sin_addr);
      
        if(connect(sockfd,(struct sockaddr *)(&server_addr),sizeof(struct sockaddr))<0) {
            fprintf(stderr,"Connect Error:%s\a\n",strerror(errno));
            usleep(100000);//100ms
            close(sockfd);
            continue;
        }
        close(sockfd);
        return 0;
    }
    return -1;
}

