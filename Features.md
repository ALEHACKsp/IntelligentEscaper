#### Escape

Calls the [escape](http://www.w3schools.com/jsref/jsref_escape.asp) Javascript function, which encodes the special characters (with some exceptions).

Example:

Hello, this is a test

Hello%2C%20this%20is%20a%20test

#### Escape (Forced)

Encodes **every** character, no matter whether it is special or not. Ideal to make the strings not easily readable.

Example:

Hello, this is a test

%48%65%6C%6C%6F%2C%20%74%68%69%73%20%69%73%20%61%20%74%65%73%74

#### Unescape

Calls the [unescape](http://www.w3schools.com/jsref/jsref_unescape.asp) Javascript function, which decodes every encoded character. (The ones with the form %XX or %uXXXX).

Example 1:

Hello%2C%20this%20is%20a%20test

Hello, this is a test

Example 2:

%48%65%6C%6C%6F%2C%20%74%68%69%73%20%69%73%20%61%20%74%65%73%74

Hello, this is a test

#### Escape URL

Encodes the special characters, but respecting the URL syntax. It’s not the same as the “encodeURI” option, since this latter one calls the [encodeURI](http://www.w3schools.com/jsref/jsref_encodeURI.asp) Javascript function, which does not encode the following characters:

; , / ? : @ & = + $ - _ . ! ~ * ' ( ) #

While this one just does not encode them where corresponding. Parameters, anchors and Unicode characters are supported…

Example:

http://www.example.com:84/This is my file named niño: the named place.doc

http://www.example.com:84/This%20is%20my%20file%20named%20ni%F1o%3A%20the%20named%20place.doc

#### Escape URL (Forced)

Similar to “Escape URL”, but forcing the normal character encoding as much as possible (without including the host). Ideal to make URLs hard to read at first glance.

Example:

http://www.example.com:84/This is my file named niño: the named place.doc

http://www.example.com:84/%54%68%69%73%20%69%73%20%6D%79%20%66%69%6C%65%20%6E%61%6D%65%64%20%6E%69%F1%6F%3A%20%74%68%65%20%6E%61%6D%65%64%20%70%6C%61%63%65%2E%64%6F%63

#### Escape URL (Forced + Host)

Similar to “Escape URL”, but forcing the normal character encoding as much as possible (host included). Ideal to make URLs hard to read at first glance.

**Note:** this syntax is not supported in some browsers.

_Supported browsers:_ Internet Explorer, Google Chrome.  
_Unsupported browsers:_ Firefox, Lynx.

In the case of Firefox, the string gets decoded correctly (if the URL is valid), but it doesn’t open, you have to open it again with the decoded address (selecting it and pressing Enter). If the host doesn’t contain any dot, “www.” y “.com” al added automatically.

If you have support information for any other browser, please tell me and I’ll add it.

Ejemplo:

http://www.example.com:84/This is my file named niño: the named place.doc

http://%77%77%77.%65%78%61%6D%70%6C%65.%63%6F%6D:84/%54%68%69%73%20%69%73%20%6D%79%20%66%69%6C%65%20%6E%61%6D%65%64%20%6E%69%F1%6F%3A%20%74%68%65%20%6E%61%6D%65%64%20%70%6C%61%63%65%2E%64%6F%63

#### Intelligent Escape

This function is an intelligent version of “Escape”. It encodes the corresponding characters, but without re-encoding the already encoded data.

Example:

Partially%22%20encoded%20string. And I added this part.

Partially%22%20encoded%20string.%20And%20I%20added%20this%20part.

If we just pressed “Escape”, we would have received:

Partially%2522%2520encoded%2520string.%20And%20I%20added%20this%20part.

#### Intelligent Unescape

Calls the [unescape](http://www.w3schools.com/jsref/jsref_unescape.asp) Javascript function repeatedly until there’s nothing remaining to be decoded. Useful when a string was encoded several times.

Example:

%25%34%38%25%36%35%25%36%43%25%36%43%25%36%46

Hello

If we just pressed “Unescape”, we would have received:

%48%65%6C%6C%6F

#### Parse URL Parameters

It breaks a URL into its corresponding parameters. The anchor is left without unescaping in the end.

Example:

http://www.example.com:81/path/path/hello.php?a=3&b=Good%20Morning&c=El%20Ni%F1o#test-anchor

http://www.example.com:81/path/path/hello.php

a=3
b=Good Morning
c=El Niño

#test-anchor


#### Make URL with Parameters

Reconstructs a URL based in the specified URL + parameters + anchor.

Example:

http://www.example.com:81/path/path/hello.php

a=3
b=Good Morning
c=El Niño

#test-anchor


http://www.example.com:81/path/path/hello.php?a=3&b=Good%20Morning&c=El%20Ni%F1o#test-anchor

#### Escape +

It encodes the + characters. They are not escaped by the other functions, with the exception of “Escape (Forced)”.

#### Escape #

It encodes the # characters. They are not escaped by the other functions, with the exception of “Escape (Forced)”.

#### Escape: 

It encodes the characters specified in the input box.

#### encodeURI

Calls the [encodeURI](https://developer.mozilla.org/en/Core_JavaScript_1.5_Reference/Global_Functions/encodeURI) Javascript function.

#### decodeURI

Calls the [decodeURI](https://developer.mozilla.org/en/Core_JavaScript_1.5_Reference/Global_Functions/decodeURI) Javascript function.

#### encodeURIComponent

Calls the [encodeURIComponent](https://developer.mozilla.org/en/Core_JavaScript_1.5_Reference/Global_Functions/encodeURIComponent) Javascript function.

#### decodeURIComponent

Calls the [decodeURIComponent](https://developer.mozilla.org/en/Core_JavaScript_1.5_Reference/Global_Functions/decodeURIComponent) Javascript function.