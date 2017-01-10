#import "acattios-unity-wrapper.h"


static acattsiosunitywrapper* delegate = nil;

#define version "1.0.0.0-beta-1"


extern "C" {
    
    
    
    static AcapelaSpeech          *acaTTS;
    static AcapelaLicense         *acaLicense;
    static AcapelaSetup           *acaSetupData;
    static NSMutableArray         *voiceslist;
    
    char* cStringCopy(const char* string)
    {
        if (string == NULL)
            return NULL;
        
        char* res = (char*)malloc(strlen(string) + 1);
        strcpy(res, string);
        
        return res;
    }
    
    
    typedef void (*callback_event)(int type, int param1, int param2, int param3, int param4);
    callback_event m_event_callback;
    
    char * init (callback_event event_callback) {
        
        m_event_callback = event_callback;
        
        
        if (delegate == nil)
            delegate = [[acattsiosunitywrapper alloc] init];
        
        
        acaLicense = [AcapelaLicense alloc];
        acaTTS = [AcapelaSpeech alloc];
        
        acaSetupData = [[AcapelaSetup alloc] initialize];
        
        id license = [acaLicense initLicense:[acattsioslicense license] user:(int)[acattsioslicense userid] passwd:(int)[acattsioslicense password]];
        if (license == nil) {
            NSLog(@"license error");
            return nil;
        }
        
        NSError *err;
        NSDictionary *dict = [acaTTS objectForProperty:AcapelaSpeechSynthesizerInfoProperty error:&err];
        NSLog(@"version: %@", [dict objectForKey:AcapelaSpeechSynthesizerInfoVersion]);
        
        
        voiceslist = [NSMutableArray arrayWithArray:acaSetupData.Voices];
        if ([voiceslist count] > 0) {
            
            int count = (int)[voiceslist count];
            NSString * voicelist = [[NSString alloc]init];
            
            for (unsigned i = 0; i < count; i++) {
                
                // Add separator
                if (i > 0)
                    voicelist = [voicelist stringByAppendingString:@":"];
                
                voicelist = [voicelist stringByAppendingString:[voiceslist objectAtIndex:i]];
                
            }
            return cStringCopy([voicelist UTF8String]);
            
        }
        
        return nil;
        
    }
    
    int loadvoice(char *voiceid) {
        
        NSString *voice = [NSString stringWithFormat:@"%s", voiceid];
        
        NSDictionary *voiceAttributesDic;
        voiceAttributesDic = [AcapelaSpeech attributesForVoice:voice];
        
        id init = [acaTTS initWithVoice:voice license:acaLicense];
        if (init == nil)
            return -1;
        
        [acaTTS setDelegate:delegate];
        
        NSString *VoiceDataVersion = [voiceAttributesDic valueForKey:AcapelaVoiceDataVersion];
        NSLog(@"voice version : %@",VoiceDataVersion);
        
        return 0;
    }
    
    void speak(const char * text) {
        
         [acaTTS startSpeakingString:[NSString stringWithUTF8String: text]];
        
    }
    
    void stop() {
        
        [acaTTS stopSpeakingAtBoundary:AcapelaSpeechImmediateBoundary];
    }
    
}



@implementation acattsiosunitywrapper

- (id)init {
    self = [super init];
    return self;
}


- (void)speechSynthesizer:(AcapelaSpeech*)synth didFinishSpeaking:(BOOL)finishedSpeaking textIndex:(int)index {
    
    m_event_callback(0,(int)finishedSpeaking,index,0,0);
}

- (void)speechSynthesizer:(AcapelaSpeech*)synth willSpeakWord:(NSRange)characterRange ofString:(NSString *)string {
    
    m_event_callback(1,(int)characterRange.location,(int)characterRange.length,0,0);
}

- (void)speechSynthesizer:(AcapelaSpeech *)sender willSpeakViseme:(short)visemeCode {
    
    m_event_callback(2,(int)visemeCode,0,0,0);
}


@end



