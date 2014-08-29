//
//  settirebarview.h
//  lianjie
//
//  Created by apple on 13-4-25.
//  Copyright (c) 2013年 John E. Ray. All rights reserved.
//

#import <UIKit/UIKit.h>

@interface settirebarview : UIViewController
{
    NSUserDefaults *userdefaults;
}
- (IBAction)closesetbar:(id)sender;

- (IBAction)hidekeyboard:(id)sender;

- (IBAction)savevalue:(id)sender;

//@property (retain, nonatomic) IBOutlet UITextField *LFMax;
@property (retain, nonatomic) IBOutlet UITextField *LFMin;
//@property (retain, nonatomic) IBOutlet UITextField *LFTemp;


//@property (retain, nonatomic) IBOutlet UITextField *RFMax;
@property (retain, nonatomic) IBOutlet UITextField *RFMin;
//@property (retain, nonatomic) IBOutlet UITextField *RFTemp;


//@property (retain, nonatomic) IBOutlet UITextField *LBMax;
@property (retain, nonatomic) IBOutlet UITextField *LBMin;
//@property (retain, nonatomic) IBOutlet UITextField *LBTemp;

//@property (retain, nonatomic) IBOutlet UITextField *RBMax;
@property (retain, nonatomic) IBOutlet UITextField *RBMin;
//@property (retain, nonatomic) IBOutlet UITextField *RBTemp;


@end

#define MAX_TEMP    70

