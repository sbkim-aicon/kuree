
//  AcapelaSetup.m - Acapela Group

#import "AcapelaSetup.h"

@implementation AcapelaSetup
@synthesize Voices;
@synthesize CurrentVoice;
@synthesize CurrentVoiceName;

-(id)initialize
{
	Voices = [NSMutableArray arrayWithArray:[AcapelaSpeech availableVoices]];  
	if (Voices.count > 0)
		CurrentVoiceName = [self SetCurrentVoice:0];
	else
		CurrentVoice = NULL;
	return self;
}

- (NSString*)SetCurrentVoice:(NSInteger)row
{
	CurrentVoice = [Voices objectAtIndex:row];
	NSDictionary *dic = [AcapelaSpeech attributesForVoice:CurrentVoice];
	CurrentVoiceName = [dic valueForKey:AcapelaVoiceName]; 
	return CurrentVoiceName;
}


- (NSString*)GetCurrentVoiceName
{
    return CurrentVoiceName;
}



@end
