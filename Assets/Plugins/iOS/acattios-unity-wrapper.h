#import <Foundation/Foundation.h>
#import "AcapelaSetup.h"
#import "acattsioslicense.h"


@interface acattsiosunitywrapper : NSObject {
 
}

- (void)speechSynthesizer:(AcapelaSpeech*)synth didFinishSpeaking:(BOOL)finishedSpeaking textIndex:(int)index;
- (void)speechSynthesizer:(AcapelaSpeech*)synth willSpeakWord:(NSRange)characterRange ofString:(NSString *)string;
- (void)speechSynthesizer:(AcapelaSpeech *)sender willSpeakViseme:(short)visemeCode;

@end

