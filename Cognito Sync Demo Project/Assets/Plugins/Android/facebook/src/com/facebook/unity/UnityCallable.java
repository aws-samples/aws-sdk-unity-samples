/**
 * 
 */
package com.facebook.unity;

import java.lang.annotation.ElementType;
import java.lang.annotation.Target;

/**
 * @author abrady
 * This annotation declares that the function in question is allowed to be called
 * from within the Unity engine.
 */
@Target({ElementType.METHOD}) 
public @interface UnityCallable {

}
