//
//  settirebarview.m
//  lianjie
//
//  Created by apple on 13-4-25.
//  Copyright (c) 2013年 John E. Ray. All rights reserved.
//

#import "settirebarview.h"

@interface settirebarview ()

@end

@implementation settirebarview






- (id)initWithNibName:(NSString *)nibNameOrNil bundle:(NSBundle *)nibBundleOrNil
{
    self = [super initWithNibName:nibNameOrNil bundle:nibBundleOrNil];
    if (self) {
        // Custom initialization
    }
    return self;
}



float stdbarvaule[4];

- (void)viewDidLoad
{
    [super viewDidLoad];
	// Do any additional setup after loading the view.
    
    userdefaults=[NSUserDefaults standardUserDefaults];
    
    //NSString *str=[NSString stringWithString:@"hahaha"];
    
    /*[userdefaults setObject:str forKey:@"myKey"];
    [userdefaults synchronize];
    NSString *value;
    value = [userdefaults stringForKey:@"myKey"];*/
    
   /* self.LFMax.text=[NSString stringWithFormat:@"%.1f",maxbarvaule[0]];
    self.RFMax.text=[NSString stringWithFormat:@"%.1f",maxbarvaule[1]];
    self.LBMax.text=[NSString stringWithFormat:@"%.1f",maxbarvaule[2]];
    self.RBMax.text=[NSString stringWithFormat:@"%.1f",maxbarvaule[3]];*/
    
    self.LFMin.text=[NSString stringWithFormat:@"%.1f",stdbarvaule[0]];
    self.RFMin.text=[NSString stringWithFormat:@"%.1f",stdbarvaule[1]];
    self.LBMin.text=[NSString stringWithFormat:@"%.1f",stdbarvaule[2]];
    self.RBMin.text=[NSString stringWithFormat:@"%.1f",stdbarvaule[3]];
    
  /*  self.LFTemp.text=[NSString stringWithFormat:@"%.1f",tempbarvaule[0]];
    self.RFTemp.text=[NSString stringWithFormat:@"%.1f",tempbarvaule[1]];
    self.LBTemp.text=[NSString stringWithFormat:@"%.1f",tempbarvaule[2]];
    self.RBTemp.text=[NSString stringWithFormat:@"%.1f",tempbarvaule[3]];*/
    

    
}

- (void)didReceiveMemoryWarning
{
    [super didReceiveMemoryWarning];
    // Dispose of any resources that can be recreated.
}

extern char laststate,nextstate;
- (IBAction)closesetbar:(id)sender{
    
    laststate=nextstate=0;
    UIStoryboard *mainstory=[UIStoryboard storyboardWithName:@"MainStoryboard" bundle:nil];
    UIViewController *homeview=[mainstory instantiateViewControllerWithIdentifier:@"homeview"];
    homeview.modalTransitionStyle=UIModalTransitionStyleCoverVertical;
    homeview.modalPresentationStyle=UIModalPresentationFormSheet;
    
    [self presentViewController:homeview animated:YES completion:nil];
    
}

- (IBAction)hidekeyboard:(id)sender {
    
   // [self.LFMax resignFirstResponder];
    [self.LFMin resignFirstResponder];
  //  [self.LFTemp resignFirstResponder];
    
  //  [self.RFMax resignFirstResponder];
    [self.RFMin resignFirstResponder];
   // [self.RFTemp resignFirstResponder];
    
 //   [self.LBMax resignFirstResponder];
    [self.LBMin resignFirstResponder];
 //  [self.LBTemp resignFirstResponder];
    
  //  [self.RBMax resignFirstResponder];
    [self.RBMin resignFirstResponder];
   // [self.RBTemp resignFirstResponder];
}


extern float maxbarvaule[4];
extern float minbarvaule[4];
extern float tempbarvaule[4];



- (IBAction)savevalue:(id)sender {
    
    float ret;
   // [userdefaults setFloat:[self.LFMax.text floatValue] forKey:@"LFMax"];
    [userdefaults setFloat:[self.LFMin.text floatValue] forKey:@"LFMin"];
   // [userdefaults setFloat:[self.LFTemp.text floatValue] forKey:@"LFTemp"];
    
   // [userdefaults setFloat:[self.RFMax.text floatValue] forKey:@"RFMax"];
    [userdefaults setFloat:[self.RFMin.text floatValue] forKey:@"RFMin"];
   // [userdefaults setFloat:[self.RFTemp.text floatValue] forKey:@"RFTemp"];
    
    
  //  [userdefaults setFloat:[self.LBMax.text floatValue] forKey:@"LBMax"];
    [userdefaults setFloat:[self.LBMin.text floatValue] forKey:@"LBMin"];
  //  [userdefaults setFloat:[self.LBTemp.text floatValue] forKey:@"LBTemp"];
    
   // [userdefaults setFloat:[self.RBMax.text floatValue] forKey:@"RBMax"];
    [userdefaults setFloat:[self.RBMin.text floatValue] forKey:@"RBMin"];
   // [userdefaults setFloat:[self.RBTemp.text floatValue] forKey:@"RBTemp"];

    [userdefaults synchronize];
    
    
    ret=stdbarvaule[0]=[self.LFMin.text floatValue];
    maxbarvaule[0]=ret+0.4*ret;
    minbarvaule[0]=ret-0.2*ret;
    
    ret=stdbarvaule[1]=[self.RFMin.text floatValue];
    maxbarvaule[1]=ret+0.4*ret;
    minbarvaule[1]=ret-0.2*ret;
    
    
    ret=stdbarvaule[2]=[self.LBMin.text floatValue];
    maxbarvaule[2]=ret+0.4*ret;
    minbarvaule[2]=ret-0.2*ret;
    
    ret=stdbarvaule[3]=[self.RBMin.text floatValue];
    maxbarvaule[3]=ret+0.4*ret;
    minbarvaule[3]=ret-0.2*ret;
    
    tempbarvaule[0]=MAX_TEMP;
    tempbarvaule[1]=MAX_TEMP;
    tempbarvaule[2]=MAX_TEMP;
    tempbarvaule[3]=MAX_TEMP;
    
}





- (void)dealloc {
   // [_LFMax release];
    [_LFMin release];
  //  [_LFTemp release];
 //   [_RFMax release];
    [_RFMin release];
 //   [_RFTemp release];
 //   [_LBMax release];
    [_LBMin release];
 //   [_LBTemp release];
 //   [_RBMax release];
    [_RBMin release];
 //   [_RBTemp release];
    [super dealloc];
}
@end
