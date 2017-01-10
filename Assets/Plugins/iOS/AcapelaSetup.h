
//  AcapelaSetup.h - Acapela Group

#import <UIKit/UIKit.h>
#import "AcapelaSpeech.h"

@interface AcapelaSetup : NSObject {
    
	NSMutableArray *Voices;
	NSString *CurrentVoice;
	NSString *CurrentVoiceName;
    
}

@property (nonatomic, retain) NSMutableArray *Voices;
@property (nonatomic, retain) NSString *CurrentVoice;
@property (nonatomic, retain) NSString *CurrentVoiceName;

- (id)initialize;
- (NSString*)SetCurrentVoice:(NSInteger)row;
- (NSString*)GetCurrentVoiceName;

@end
