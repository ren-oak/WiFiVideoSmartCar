//
//  ViewController.m
//  Disconnected
//


#import "ViewController.h"
#import <AudioToolbox/AudioToolbox.h>


@implementation ViewController


@synthesize flowerView;
@synthesize modechoice;
@synthesize _motionManager;





#pragma mark - View lifecycle


- (void)loadwebview
{
    NSString *outputHTML;
   	// Do any additional setup after loading the view, typically from a nib.
    
    outputHTML=[[NSString alloc] initWithFormat:@"<body style='margin: 0px; padding: 0px'><img width='1200' height='800' src='http://192.168.10.1:8080/?action=stream'></body>"];

    [flowerView loadHTMLString:outputHTML baseURL:nil];
    
    
   /* NSString *HTMLData = @"<body style='margin: 0px; padding: 0px'><img width='1200' height='800' src='http://192.168.10.1:8080/?action=stream'></body>";
    flowerView.scalesPageToFit = YES;
    [flowerView loadHTMLString:HTMLData baseURL:[NSURL fileURLWithPath: [[NSBundle mainBundle]  bundlePath]]];*/
    
    
    /*NSURL *urlBai=[NSURL URLWithString:@"http://www.baidu.com"];
    NSURLRequest *request=[NSURLRequest requestWithURL:urlBai];
    flowerView.scalesPageToFit = YES;
    [flowerView loadRequest:request];*/

    
}




-(void)doAccess:(CMAcceleration) acceleration{
    int mode;
    Byte cmd[] = {'0','1','2','3','4','5','6','7','8','9','a','b','c'};
    int result;
	
    if(acceleration.x>1.3)
        result=4;
    else if(acceleration.x<-1.3)
        result=3;
   /* if(acceleration.y>1.3)
        result=3;
    else if(acceleration.y<-1.3)
        result=4;*/
    
    else
        result=0;
    
    
	switch (mode) {
        case 0://行车
            result=result;
            
            break;
		case 1://云台
			if(result)
                result+=4;
            
			break;
    }
    
    //if(result)
      //  udpsendcmd(cmd[result]);
    
	
}





- (void)viewDidLoad
{
	 [super viewDidLoad];
	// Do any additional setup after loading the view, typically from a nib.
    
    /*if(!connect_check(8000))
        [self loadwebview];*/
   /*  timer=[NSTimer scheduledTimerWithTimeInterval:1
     target:self
     selector:@selector(timeoutprocess)
     userInfo:nil
     repeats:NO];*/
    udp_init();
    
        
   _motionManager = [[CMMotionManager alloc]init];
    if (_motionManager.accelerometerAvailable) {
        [_motionManager setAccelerometerUpdateInterval:1/60.f];
        NSOperationQueue *operationQueue = [NSOperationQueue mainQueue];
        [_motionManager startAccelerometerUpdatesToQueue:operationQueue withHandler:^(CMAccelerometerData *accelData,NSError *error){
            [self doAccess:accelData.acceleration];
                     }];
    }
    
    
    

  
}


- (IBAction)quitback:(id)sender {
    //[self dismissViewControllerAnimated:YES completion:nil];
    printf("video");
   [self loadwebview];
  
    
}
static char led=0;

- (IBAction)switchled:(id)sender {
    if(led){
        udpsendcmd('b');
        led=0;
         [self.switchbtn setImage:[UIImage imageNamed:@"off.png"] forState:UIControlStateNormal];
    
    }else{
        udpsendcmd('a');
        led=1;
        [self.switchbtn setImage:[UIImage imageNamed:@"on.png"] forState:UIControlStateNormal];
    }
}



- (IBAction)speakerdown:(id)sender {udpsendcmd('c');
}

- (IBAction)speakerup:(id)sender { udpsendcmd('d');
}

- (IBAction)backtouchdown:(id)sender {
    int mode=modechoice.selectedSegmentIndex;
    if(!mode) udpsendcmd('2');
    else udpsendcmd('6');
}

- (IBAction)backtouchup:(id)sender { udpsendcmd('0');
}

- (IBAction)forwardtouchdown:(id)sender {
    int mode=modechoice.selectedSegmentIndex;
    if(!mode) udpsendcmd('1');
    else udpsendcmd('5');
}

- (IBAction)forwardtouchup:(id)sender {udpsendcmd('0');
}

- (IBAction)righttouchdown:(id)sender {
    int mode=modechoice.selectedSegmentIndex;
    if(!mode) udpsendcmd('4');
    else udpsendcmd('8');
}

- (IBAction)righttouchup:(id)sender {udpsendcmd('0');
}

- (IBAction)lefttouchdown:(id)sender {
    int mode=modechoice.selectedSegmentIndex;
    if(!mode) udpsendcmd('3');
    else udpsendcmd('7');
}

- (IBAction)lefttouchup:(id)sender {udpsendcmd('0');
}


-(void)webView:(UIWebView *)webView didFailLoadWithError:(NSError *)error
{
    NSLog(@"Loader Error!");
}





-(void)webViewDidStartLoad:(UIWebView*)webView{
    
    NSLog(@"DidStartLoad");
    
    
}
-(void)webViewDidFinishLoad:(UIWebView*)webView {
   
        NSLog(@"DidFinish");
    
     /*   if(!timeout){
            [timer invalidate];
        }*/


       [self loadwebview];


}

- (void)viewDidUnload
{
    [self setflowerView:nil];
    [self setChosenColor:nil];
    [self setColorChoice:nil];
    [self motion:nil];

    [self setModechoice:nil];
    [super viewDidUnload];
    // Release any retained subviews of the main view.
    // e.g. self.myOutlet = nil;
}

- (void)viewWillAppear:(BOOL)animated
{
    [super viewWillAppear:animated];
}

- (void)viewDidAppear:(BOOL)animated
{
    [super viewDidAppear:animated];
}

- (void)viewWillDisappear:(BOOL)animated
{
	[super viewWillDisappear:animated];
}

- (void)viewDidDisappear:(BOOL)animated
{
	[super viewDidDisappear:animated];
}

- (BOOL)shouldAutorotateToInterfaceOrientation:(UIInterfaceOrientation)interfaceOrientation
{
    // Return YES for supported orientations
    return (interfaceOrientation != UIInterfaceOrientationPortraitUpsideDown);
}

- (void)dealloc {
    [modechoice release];
    [_speaker release];
    [_switchbtn release];
    [super dealloc];
}
@end
