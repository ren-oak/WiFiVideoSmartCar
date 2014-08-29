//
//  AppDelegate.m
//  Disconnected
//


#import "AppDelegate.h"
#import "ViewController.h"

@implementation AppDelegate


@synthesize window = _window;

@synthesize udpSocket;


AppDelegate *sysdgt;



//socket process
//first of all,init udp socket
-(BOOL) initUdpServer
{
    //初始化udp
	self.udpSocket = [[[AsyncUdpSocket alloc] initWithDelegate: self] autorelease];
	//绑定端口
	NSError *error = nil;
    BOOL result = [self.udpSocket bindToPort: 1376 error: &error];
    if (result){
        //启动接收线程
        [self.udpSocket receiveWithTimeout: -1 tag: 8];
    }
    
    return result;
}



- (BOOL)onUdpSocket:(AsyncUdpSocket *)sock didReceiveData:(NSData *)data withTag:(long)tag fromHost:(NSString *)host port:(UInt16)port
{
       return YES;
}


/**
 * Called when the datagram with the given tag has been sent.
 **/
- (void)onUdpSocket:(AsyncUdpSocket *)sock didSendDataWithTag:(long)tag
{
    
    NSLog(@"didSendDataWithTag:(long)tag=%ld",tag);
    
}

/**
 * Called if an error occurs while trying to send a datagram.
 * This could be due to a timeout, or something more serious such as the data being too large to fit in a sigle packet.
 **/

- (BOOL)onUdpSocket:(AsyncUdpSocket *)sock didNotSendDataWithTag:(long)tag dueToError:(NSError *)error
{
    NSLog(@"%s %d", __FUNCTION__, __LINE__);
    return YES;
}


/**
 * Called if an error occurs while trying to receive a requested datagram.
 * This is generally due to a timeout, but could potentially be something else if some kind of OS error occurred.
 **/
- (void)onUdpSocket:(AsyncUdpSocket *)sock didNotReceiveDataWithTag:(long)tag dueToError:(NSError *)error
{
    NSLog(@"%s %d, tag = %ld, error = %@", __FUNCTION__, __LINE__, tag, error);
    
}

/**
 * Called when the socket is closed.
 * A socket is only closed if you explicitly call one of the close methods.
 **/
- (void)onUdpSocketDidClose:(AsyncUdpSocket *)sock
{
    NSLog(@"%s %d", __FUNCTION__, __LINE__);
}


- (void)dealloc
{
    self.udpSocket = nil;
    
    
    [super dealloc];
}








//delegate


- (void)applicationWillResignActive:(UIApplication *)application
{
    /*
     Sent when the application is about to move from active to inactive state. This can occur for certain types of temporary interruptions (such as an incoming phone call or SMS message) or when the user quits the application and it begins the transition to the background state.
     Use this method to pause ongoing tasks, disable timers, and throttle down OpenGL ES frame rates. Games should use this method to pause the game.
     */
    
}

- (void)applicationDidEnterBackground:(UIApplication *)application
{
    /*
     Use this method to release shared resources, save user data, invalidate timers, and store enough application state information to restore your application to its current state in case it is terminated later. 
     If your application supports background execution, this method is called instead of applicationWillTerminate: when the user quits.
     */
    
    
    
}

- (void)applicationWillEnterForeground:(UIApplication *)application
{
    /*
     Called as part of the transition from the background to the inactive state; here you can undo many of the changes made on entering the background.
     */
}

- (void)applicationDidBecomeActive:(UIApplication *)application
{
    /*
     Restart any tasks that were paused (or not yet started) while the application was inactive. If the application was previously in the background, optionally refresh the user interface.
     */
    
    
}

- (void)applicationWillTerminate:(UIApplication *)application
{
    /*
     Called when the application is about to terminate.
     Save data if appropriate.
     See also applicationDidEnterBackground:.
     */
    
}




- (BOOL)application:(UIApplication *)application didFinishLaunchingWithOptions:(NSDictionary *)launchOptions
{
    sysdgt=self;
    
   /* NSString *filestr=[avipath stringByAppendingPathComponent:@"ios.avi"];
    char *avifile=[filestr cStringUsingEncoding:NSASCIIStringEncoding];*/

    [[UIApplication sharedApplication]setIdleTimerDisabled:YES];
      
   
    return YES;
}





@end
