static char* cloneString(const char * c)
{
    char* res = (char*)malloc(strlen(c)+1);
    strcpy(res, c);
    return res;
}

extern "C" {
    
    char* locale()
    {
        NSString *locale = [[NSLocale autoupdatingCurrentLocale] localeIdentifier];
        const char *l = [locale UTF8String];
        return cloneString(l);
    }
    
    char* title()
    {
        NSString *bundleDisplayName = [[[NSBundle mainBundle] infoDictionary] objectForKey:@"CFBundleDisplayName"];
        const char *c = [bundleDisplayName UTF8String];
        return cloneString(c);
    }
    
    char* packageName()
    {
        NSString *bundleIdentifier = [[[NSBundle mainBundle] infoDictionary] objectForKey:@"CFBundleIdentifier"];
        const char *p = [bundleIdentifier UTF8String];
        return cloneString(p);
    }
    
    char* versionCode()
    {
        NSString *shortVersion = [[[NSBundle mainBundle] infoDictionary] objectForKey:@"CFBundleVersion"];
        const char *v = [shortVersion UTF8String];
        return cloneString(v);
    }
    
    char* versionName()
    {
        NSString *bundleVersionString = [[[NSBundle mainBundle] infoDictionary] objectForKey:@"CFBundleShortVersionString"];
        const char *vn = [bundleVersionString UTF8String];
        return cloneString(vn);
    }
    
}



