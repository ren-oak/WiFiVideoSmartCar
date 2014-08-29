//
//  TireVeiwControl.m
//  lianjie
//
//  Created by apple on 13-4-12.
//  Copyright (c) 2013年 John E. Ray. All rights reserved.
//


#import "TireVeiwControl.h"
#import "ViewController.h"


extern char laststate,nextstate;


@implementation TireVeiwControl

@synthesize  LFBarColor;//clore box
@synthesize  RFBarColor;
@synthesize  LBBarColor;
@synthesize  RBBarColor;


@synthesize  LFBarValue;
@synthesize  RFBarValue;
@synthesize  LBBarValue;
@synthesize  RBBarVaule;


@synthesize  LFBarTemp;
@synthesize  RFBarTemp;
@synthesize  LBBarTemp;
@synthesize  RBBarTemp;

@synthesize  WarmBar;
@synthesize  WarmReason;


float TireBarValue[4]={2.1,2.5,3.0,4.5};
float TireBarTemp[4]={46,45,47,62};


float maxbarvaule[4];
float minbarvaule[4];
float tempbarvaule[4];

//process tirebar display
void dispalayitem(UITextField *item,UITextField *valueitem,UITextField *tempitem,
                  int index,float vaule,float temp)
{

    if(vaule>=maxbarvaule[index]||vaule<=minbarvaule[index]||temp>=tempbarvaule[index]){
        
        item.backgroundColor = [UIColor redColor];
        
    }else{
        item.backgroundColor = [UIColor greenColor];
    }
    
    valueitem.text=[NSString localizedStringWithFormat:@"%.1f",vaule];//show tire vaule
    
    tempitem.text=[NSString localizedStringWithFormat:@"%.0f℃",temp];
    
}

NSString *numbertostring(int i)
{
    switch(i)
    {
        case 0:
            return @"左前轮";
        break;
        case 1:
            return @"右前轮";
        break;
        case 2:
            return @"左后轮";
        break;
        case 3:
            return @"右后轮";
            break;
    }
}




-(void)dispalaybarinfo:(float *)vaule tempa:(float *)temp
{

    //[self dispalayitem:LFBarColor valuetext:LFBarValue temptext:LFBarTemp vaul:3.8 temps:55];
    char i=0;
    dispalayitem(self.LFBarColor,self.LFBarValue,self.LFBarTemp,0,TireBarValue[0],TireBarTemp[0]);
    dispalayitem(self.RFBarColor,self.RFBarValue,self.RFBarTemp,1,TireBarValue[1],TireBarTemp[1]);
    dispalayitem(self.LBBarColor,self.LBBarValue,self.LBBarTemp,2,TireBarValue[2],TireBarTemp[2]);
    dispalayitem(self.RBBarColor,self.RBBarVaule,self.RBBarTemp,3,TireBarValue[3],TireBarTemp[3]);
    for(i=0;i<4;i++){
        if(TireBarValue[i]<minbarvaule[i]){
            self.WarmBar.text=[NSString localizedStringWithFormat:@"%@",numbertostring(i)];//show tire vaule
            self.WarmReason.text=[NSString localizedStringWithFormat:@"%@",@"胎压低"];//show tire vaule
            return;
            
        }  else if(TireBarValue[i]>maxbarvaule[i]){
            
            self.WarmBar.text=[NSString localizedStringWithFormat:@"%@",numbertostring(i)];//show tire vaule
            self.WarmReason.text=[NSString localizedStringWithFormat:@"%@",@"胎压高"];//show tire vaule
            return;
            
        } else if(TireBarTemp[i]>tempbarvaule[i]){
            
            self.WarmBar.text=[NSString localizedStringWithFormat:@"%@",numbertostring(i)];//show tire vaule
            self.WarmReason.text=[NSString localizedStringWithFormat:@"%@",@"胎温高"];//show tire vaule
            return;    
            
        }
    }
    if(i>=4){
        self.WarmBar.text=[NSString localizedStringWithFormat:@"   "];//show tire vaule
        self.WarmReason.text=[NSString localizedStringWithFormat:@"   "];//show tire vaule
    }
  

}
//QUIT
- (IBAction)senceback:(id)sender {
    
    //[self dismissViewControllerAnimated:YES completion:nil];
    
    laststate=nextstate=0;
    UIStoryboard *mainstory=[UIStoryboard storyboardWithName:@"MainStoryboard" bundle:nil];
    ViewController *homeview=[mainstory instantiateViewControllerWithIdentifier:@"homeview"];
    homeview.modalTransitionStyle=UIModalTransitionStyleCoverVertical;
    homeview.modalPresentationStyle=UIModalPresentationFormSheet;
    
    [self presentViewController:homeview animated:YES completion:nil];

}



- (id)initWithNibName:(NSString *)nibNameOrNil bundle:(NSBundle *)nibBundleOrNil
{
    self = [super initWithNibName:nibNameOrNil bundle:nibBundleOrNil];
    if (self) {
        // Custom initialization
    }
    return self;
}

NSTimer *timer;
//周期性处理当前胎压界面
-(void)process_bar{
    [self dispalaybarinfo:TireBarValue tempa:TireBarTemp];
    //[self performSegueWithIdentifier:@"backview2"sender:self];
    
//here is display backview
#if 0
    if(viewsence==1){
        UIStoryboard *mainstory=[UIStoryboard storyboardWithName:@"MainStoryboard" bundle:nil];
        ViewController *backview=[mainstory instantiateViewControllerWithIdentifier:@"backview"];
        backview.modalTransitionStyle=UIModalTransitionStyleCoverVertical;
        backview.modalPresentationStyle=UIModalPresentationFormSheet;
        [self presentViewController:backview animated:YES completion:nil];
        //[self presentedModleViewController:backview animated:YES completion:nil];
        //[self dismissViewControllerAnimated:YES completion:nil];
    }
#endif
    
}



- (void)viewDidLoad
{
    [super viewDidLoad];
	// Do any additional setup after loading the view.
    
    //NSString *msg= [self initUdpServer] ? @"OK" : @"Fail!";
   // NSLog(@"start udp:%@",msg);

    [self dispalaybarinfo:TireBarValue tempa:TireBarTemp];
    
    timer=[NSTimer scheduledTimerWithTimeInterval:0.1
                                           target:self
                                         selector:@selector(process_bar)
                                         userInfo:nil
                                          repeats:YES];				
  
 
}

- (void)didReceiveMemoryWarning
{
    [super didReceiveMemoryWarning];
    // Dispose of any resources that can be recreated.
}

- (void)viewDidUnload {
    [self setLFBarColor:nil];
    [self setRFBarColor:nil];
    [self setLBBarColor:nil];
    [self setRBBarColor:nil];
    [self setLFBarValue:nil];
    [self setRFBarValue:nil];
    [self setLBBarValue:nil];
    [self setRBBarVaule:nil];
    [self setRBBarVaule:nil];
    [self setLFBarTemp:nil];
    [self setRFBarTemp:nil];
    [self setLBBarTemp:nil];
    [self setRBBarTemp:nil];
    [self setWarmBar:nil];
    [timer invalidate];
    [timer dealloc];
    
    
    [super viewDidUnload];
}
- (IBAction)hidkeybroad:(id)sender {
    
    [self.LFBarColor resignFirstResponder];
    [self.RFBarColor resignFirstResponder];
    [self.LBBarColor resignFirstResponder];
    [self.RBBarColor resignFirstResponder];
    
    [self.LFBarValue resignFirstResponder];
    [self.RFBarValue resignFirstResponder];
    [self.LBBarValue resignFirstResponder];
    [self.RBBarVaule resignFirstResponder];
    
    
    [self.LFBarTemp resignFirstResponder];
    [self.RFBarTemp resignFirstResponder];
    [self.LBBarTemp resignFirstResponder];
    [self.RBBarTemp resignFirstResponder];

}


@end