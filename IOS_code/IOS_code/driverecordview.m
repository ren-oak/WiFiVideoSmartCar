//
//  driverecordview.m
//  lianjie
//
//  Created by apple on 13-4-25.
//  Copyright (c) 2013年 John E. Ray. All rights reserved.
//

#import "driverecordview.h"

extern char driverecord;
extern char laststate,nextstate;

@interface driverecordview ()

@end

@implementation driverecordview

@synthesize drivervideo;


- (id)initWithNibName:(NSString *)nibNameOrNil bundle:(NSBundle *)nibBundleOrNil
{
    self = [super initWithNibName:nibNameOrNil bundle:nibBundleOrNil];
    if (self) {
        // Custom initialization
        
    }
    return self;
}


- (void)loadwebview
{
    NSString *outputHTML;
   	// Do any additional setup after loading the view, typically from a nib.
    
     outputHTML=[[NSString alloc] initWithFormat:@"<body style='margin: 0px; padding: 0px'><img width='1200' height='800' src='http://192.168.10.1:8080/?action=stream'></body>"];
	[drivervideo loadHTMLString:outputHTML baseURL:nil];
}





int connect_check(int port);
- (void)viewDidLoad
{
    [super viewDidLoad];
	// Do any additional setup after loading the view.
    
    if(!connect_check(8080))
     [self loadwebview];
    driverecord=1;
}

- (void)didReceiveMemoryWarning
{
    [super didReceiveMemoryWarning];
    // Dispose of any resources that can be recreated.
}





- (void)dealloc {
    [drivervideo release];

    [super dealloc];
}





- (IBAction)closevideo:(id)sender {
    
    laststate=nextstate=0;
#if 1
    UIStoryboard *mainstory=[UIStoryboard storyboardWithName:@"MainStoryboard" bundle:nil];
    UIViewController *homeview=[mainstory instantiateViewControllerWithIdentifier:@"homeview"];
    homeview.modalTransitionStyle=UIModalTransitionStyleCoverVertical;
    homeview.modalPresentationStyle=UIModalPresentationFormSheet;
    [self presentViewController:homeview animated:YES completion:nil];
    
#else
    
    [self dismissViewControllerAnimated:YES completion:nil];
    
#endif
    
    driverecord=0;

}
@end
