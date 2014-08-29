//
//  TireVeiwControl.h
//  lianjie
//
//  Created by apple on 13-4-12.
//  Copyright (c) 2013年 John E. Ray. All rights reserved.
//

#import <UIKit/UIKit.h>

@interface TireVeiwControl : UIViewController
@property (strong, nonatomic) IBOutlet UITextField *LFBarColor;//clore box
@property (strong, nonatomic) IBOutlet UITextField *RFBarColor;
@property (strong, nonatomic) IBOutlet UITextField *LBBarColor;
@property (strong, nonatomic) IBOutlet UITextField *RBBarColor;

//bar value
@property (strong, nonatomic) IBOutlet UITextField *LFBarValue;
@property (strong, nonatomic) IBOutlet UITextField *RFBarValue;
@property (strong, nonatomic) IBOutlet UITextField *LBBarValue;
@property (strong, nonatomic) IBOutlet UITextField *RBBarVaule;

//temp
@property (strong, nonatomic) IBOutlet UITextField *LFBarTemp;
@property (strong, nonatomic) IBOutlet UITextField *RFBarTemp;
@property (strong, nonatomic) IBOutlet UITextField *LBBarTemp;
@property (strong, nonatomic) IBOutlet UITextField *RBBarTemp;


@property (strong, nonatomic) IBOutlet UITextField *WarmBar;
@property (strong, nonatomic) IBOutlet UITextField *WarmReason;




- (IBAction)hidkeybroad:(id)sender;

//+ (void) dispalayitem:(UITextField *)item valuetext:(UITextField *)valueitem temptext:(UITextField *)tempitem values:(float)vaule temps:(float)temp;

//+ (void) dispalaybarinfo:(NSMutableArray *)vaule tempa:(NSMutableArray *)temp;
- (void)dispalaybarinfo:(float *)vaule tempa:(float *)temp;

- (IBAction)senceback:(id)sender;

@end



