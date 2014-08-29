//
//  AppDelegate.h
//  Disconnected


#import <UIKit/UIKit.h>
#import "AsyncUdpSocket.h"

#import "radiodelegate.h"

@interface AppDelegate : UIResponder <UIApplicationDelegate>{
    
    AsyncUdpSocket *udpSocket;
    NSObject<radiodelegate> *radiodgt;
    
     NSUserDefaults *userdefaults;
    
}

@property (strong, nonatomic) UIWindow *window;
@property (strong, nonatomic) AsyncUdpSocket *udpSocket;
@property (nonatomic, assign) NSObject<radiodelegate> *radiodgt; 
@end
