
#import "iPhoneSpeaker.h"
#import <AVFoundation/AVFoundation.h>


void _forceToSpeaker() {
    // want audio to go to headset if it's connected
    if (_headsetConnected()) {
        return;
    }

    OSStatus error;
    UInt32 audioRouteOverride = kAudioSessionOverrideAudioRoute_Speaker;
    error = AudioSessionSetProperty(kAudioSessionProperty_OverrideAudioRoute,
                                     sizeof(audioRouteOverride),
                                     &audioRouteOverride);

    if (error) {
        NSLog(@"Audio already playing through speaker!");
    } else {
        NSLog(@"Forcing audio to speaker");
    }
}


bool _headsetConnected() {

    UInt32 routeSize = sizeof(CFStringRef);
    CFStringRef route = NULL;
    OSStatus error = AudioSessionGetProperty(kAudioSessionProperty_AudioRoute, &routeSize, &route);

    if (!error &&
        (route != NULL)&&
        ([(__bridge NSString*)route rangeOfString:@"Head"].location != NSNotFound))
    {
        CFRelease(route);

        NSLog(@"Headset connected!");
        return true;
    }
    
    if(route != NULL)
    {
        CFRelease(route);
    }

    return false;
}
