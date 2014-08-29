//
//  ViewController.h
//  Disconnected
//


#import <UIKit/UIKit.h>
#import "AsyncUdpSocket.h"
#import "radiodelegate.h"
#import "AppDelegate.h"
#import <CoreMotion/CoreMotion.h>

@interface ViewController:UIViewController{

    
}


@property (retain, nonatomic) IBOutlet UIButton *switchbtn;

@property (retain, nonatomic) IBOutlet UIButton *speaker;

@property (strong, nonatomic) IBOutlet UIWebView *flowerView;
@property (retain, nonatomic) IBOutlet UISegmentedControl *modechoice;
@property (strong, nonatomic) CMMotionManager * _motionManager;
-(IBAction)getFlower:(id)sender;
-(IBAction)quitback:(id)sender;
- (IBAction)switchled:(id)sender;
- (IBAction)speakerdown:(id)sender;
- (IBAction)speakerup:(id)sender;
- (IBAction)backtouchdown:(id)sender;
- (IBAction)backtouchup:(id)sender;
- (IBAction)forwardtouchdown:(id)sender;
- (IBAction)forwardtouchup:(id)sender;
- (IBAction)righttouchdown:(id)sender;
- (IBAction)righttouchup:(id)sender;
- (IBAction)lefttouchdown:(id)sender;
- (IBAction)lefttouchup:(id)sender;





@end



