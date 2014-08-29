//
//  HomeViewController.m
//  lianjie
//
//  Created by apple on 13-4-16.
//  Copyright (c) 2013年 John E. Ray. All rights reserved.
//

#import "HomeViewController.h"


@interface HomeViewController ()

@end

@implementation HomeViewController

- (id)initWithNibName:(NSString *)nibNameOrNil bundle:(NSBundle *)nibBundleOrNil
{
    self = [super initWithNibName:nibNameOrNil bundle:nibBundleOrNil];
    if (self) {
        // Custom initialization
    }
    return self;
}


static NSTimer *timer;
extern char driverecord;
static char timeout=0;
//周期性处理当前胎压界面
-(void)process_drivervideo{
   
    //here begin to record driver video
    UIStoryboard *mainstory=[UIStoryboard storyboardWithName:@"MainStoryboard" bundle:nil];
    UIViewController *homeview=[mainstory instantiateViewControllerWithIdentifier:@"drivervideo"];
    homeview.modalTransitionStyle=UIModalTransitionStyleCoverVertical;
    homeview.modalPresentationStyle=UIModalPresentationFormSheet;
    [self presentViewController:homeview animated:YES completion:nil];
    driverecord=1;
    timeout=1;
    
}


- (void)viewDidLoad
{
    [super viewDidLoad];
	// Do any additional setup after loading the view.
   timer=[NSTimer scheduledTimerWithTimeInterval:10
                                           target:self
                                          selector:@selector(process_drivervideo)
                                          userInfo:nil
                                          repeats:NO];
    timeout=0;
    
}

- (void)viewDidDisappear:(BOOL)animated{
    
    if(!timeout){
        [timer invalidate];
    }
    

}



- (void)didReceiveMemoryWarning
{
    [super didReceiveMemoryWarning];
    // Dispose of any resources that can be recreated.
    
    
}


@end
