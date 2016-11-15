//
// UnityCookies.cs
// by Sam McGrath
//
// Use as you please.
//
// Usage:
//    Dictionary<string,string> cookies = www.ParseCookies();
//
// To send cookies in a WWW response:
//    var www = new WWW( url, null, UnityCookies.GetCookieRequestHeader(cookies) );
//    (if other headers are needed, merge them with the dictionary returned by GetCookieRequestHeader)
//

using UnityEngine;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

public static class UnityCookies {

	public static string GetRawCookieString( this WWW www ) {
		if ( !www.responseHeaders.ContainsKey("SET-COOKIE") ) {
			return null;
		}

		// HACK: workaround for Unity bug that doesn't allow multiple SET-COOKIE headers
		var rhsPropInfo = typeof(WWW).GetProperty( "responseHeadersString",BindingFlags.Public|BindingFlags.NonPublic|BindingFlags.Instance );
		if ( rhsPropInfo == null ) {
			Debug.LogError( "www.responseHeadersString not found in WWW class." );
			return null;
		}
		var headersString = rhsPropInfo.GetValue( www, null ) as string;
		if ( headersString == null ) {
			return null;
		}

		// concat cookie headers
		var allCookies = new StringBuilder();
		string[] lines = headersString.Split( new string[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries );
		foreach( var l in lines ) {
			var colIdx = l.IndexOf( ':' );
			if ( colIdx < 1 ) {
				continue;
			}
			var headerType = l.Substring( 0,colIdx ).Trim();
			if ( headerType.ToUpperInvariant() != "SET-COOKIE" ) {
				continue;
			}
			var headerVal = l.Substring( colIdx+1 ).Trim();
			if ( allCookies.Length > 0 ) {
				allCookies.Append( "; " );
			}
			allCookies.Append( headerVal );
		}

		return allCookies.ToString();
	}

	public static Dictionary<string,string> ParseCookies( this WWW www ) {
		return ParseCookies( www.GetRawCookieString() );
	}

	public static Dictionary<string,string> ParseCookies( string str ) {
		// cookie parsing adapted from node.js cookie module, so it should be pretty robust.
		var dict = new Dictionary<string,string>();
		if ( str != null ) {
			var pairs = Regex.Split( str, "; *" );
			foreach( var pair in pairs ) {
				var eqIdx = pair.IndexOf( '=' );
				if ( eqIdx == -1 ) {
					continue;
				}
				var key = pair.Substring( 0,eqIdx ).Trim();
				if ( dict.ContainsKey(key) ) {
					continue;
				}
				var val = pair.Substring( eqIdx+1 ).Trim();
				if ( val[0] == '"' ) {
					val = val.Substring( 1, val.Length-2 );
				}
				dict[ key ] = WWW.UnEscapeURL( val );
			}
		}

		return dict;
	}

	public static Dictionary<string,string> GetCookieRequestHeader( Dictionary<string,string> cookies ) {
		var str = new StringBuilder();
		foreach( var c in cookies ) {
			if ( str.Length > 0 )
				str.Append( "; " );
			str.Append( c.Key ).Append( '=' ).Append( WWW.EscapeURL(c.Value) );
		}
		return new Dictionary<string,string>{ {"Cookie", str.ToString() } };
	}
}