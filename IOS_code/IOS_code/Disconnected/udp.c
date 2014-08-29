//
//  File.c
//  videocar
//
//  Created by apple on 13-6-1.
//  Copyright (c) 2013年 John E. Ray. All rights reserved.
//

#include <arpa/inet.h>
#include <netinet/in.h>
#include <sys/types.h>          /* See NOTES */
#include <sys/socket.h>
#include <stdio.h>
#include <strings.h>

static int sockfd;
struct sockaddr_in dest_addr;

int udp_init()
{
    
    sockfd = socket(AF_INET,SOCK_DGRAM,0);


    bzero(&dest_addr,sizeof(dest_addr));
    dest_addr.sin_family=AF_INET;
    dest_addr.sin_port=htons(1376);
    dest_addr.sin_addr.s_addr=inet_addr("192.168.10.1");
   
    return 0;
 
}

int udpsendcmd(char cmd)
{
    int ret = sendto(sockfd, &cmd, 1, 0, (struct sockaddr *)&dest_addr, sizeof(dest_addr));

    printf("send ok:%d\n",ret);
       return 0;
}