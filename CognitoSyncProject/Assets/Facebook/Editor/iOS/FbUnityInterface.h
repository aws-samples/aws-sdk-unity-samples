
#import <UIKit/UIKit.h>
#import <FacebookSDK/FacebookSDK.h>

#include "NativeDialogModes.cs"
#include "RegisterMonoModules.h"


//if we are on a version of unity that has the version number defined use it, otherwise we have added it ourselves in the post build step
#if HAS_UNITY_VERSION_DEF
  #include "UnityTrampolineConfigure.h"
#endif


#if UNITY_VERSION >= 430
#import "AppDelegateListener.h"
@interface FbUnityInterface : NSObject <AppDelegateListener>
#else
@interface FbUnityInterface : NSObject
#endif

@property (strong, nonatomic) FBSession *session;
@property (nonatomic) BOOL isInitializing;
@property (strong, nonatomic) FBFrictionlessRecipientCache *friendCache;
@property (assign, nonatomic) BOOL useFrictionlessRequests;
@property (nonatomic) NativeDialogModes::eModes dialogMode;
@property (nonatomic, strong) NSString *launchURL;

+(FbUnityInterface *)sharedInstance;
-(id)init;
-(id)initWithCookie:(bool)cookie logging:(bool)_logging status:(bool)_status frictionlessRequests:(bool)_frictionlessRequests urlSuffix:(const char *)urlSuffix;
-(void)login:(const char *)scope;
-(void)logout;

#if UNITY_VERSION >= 430
- (void)onOpenURL:(NSNotification*)notification;
#endif
@end