﻿using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Text;

namespace XHTMLr {
	public static class Entities {
		internal static Dictionary<string, int> _Entities = typeof(Entities).GetFields(System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public)
				.ToDictionary(x => x.Name, x => ((string)x.GetValue(null)).Trim('&', '#', ';').ToInt());
		internal static Dictionary<string, int> _EntitiesLower = _Entities.Keys.OrderBy(x => x) // prioritize entities in lowercase so &DAGGER; resolves to &dagger; not &Dagger;
				.Distinct(StringComparer.OrdinalIgnoreCase).ToDictionary(x => x.ToLower(), y => _Entities[y]);

		public static string Decode(string text, Encoding encoding = null) {
			return Regex.Replace(text, @"&(#?[a-zA-Z0-9]+);", m => DecodeEntity(m.Groups[1].Value, encoding) ?? m.Value);
		}

		public static string DecodeEntity(string name, Encoding encoding = null) {
			var value = 0;

			if (name.IsNullOrEmpty()) return null;

			if (name[0] == '#') {
				if (name.Length > 1 && name[1] == 'x') {
					value = name.Substring(2).HexToInt();
				} else {
					value = name.Substring(1).ToInt();
				}
				if (value == 0) return null;

			} else if (!_Entities.TryGetValue(name, out value) && !_EntitiesLower.TryGetValue(name.ToLower(), out value)) {
				return null;
			}

			var data = BitConverter.GetBytes(value);
			if (BitConverter.IsLittleEndian) {
				Array.Reverse(data);
			}

			return (encoding ?? Encoding.Default).GetString(data);
		}


		/// <summary>no-break space = non-breaking space: &#160;</summary>
		public const string nbsp = "&#160;";
		/// <summary>inverted exclamation mark: &#161;</summary>
		public const string iexcl = "&#161;";
		/// <summary>cent sign: &#162;</summary>
		public const string cent = "&#162;";
		/// <summary>pound sign: &#163;</summary>
		public const string pound = "&#163;";
		/// <summary>currency sign: &#164;</summary>
		public const string curren = "&#164;";
		/// <summary>yen sign = yuan sign: &#165;</summary>
		public const string yen = "&#165;";
		/// <summary>broken bar = broken vertical bar: &#166;</summary>
		public const string brvbar = "&#166;";
		/// <summary>section sign: &#167;</summary>
		public const string sect = "&#167;";
		/// <summary>diaeresis = spacing diaeresis: &#168;</summary>
		public const string uml = "&#168;";
		/// <summary>copyright sign: &#169;</summary>
		public const string copy = "&#169;";
		/// <summary>feminine ordinal indicator: &#170;</summary>
		public const string ordf = "&#170;";
		/// <summary>left-pointing double angle quotation mark = left pointing guillemet: &#171;</summary>
		public const string laquo = "&#171;";
		/// <summary>not sign: &#172;</summary>
		public const string not = "&#172;";
		/// <summary>soft hyphen = discretionary hyphen: &#173;</summary>
		public const string shy = "&#173;";
		/// <summary>registered sign = registered trade mark sign: &#174;</summary>
		public const string reg = "&#174;";
		/// <summary>macron = spacing macron = overline = APL overbar: &#175;</summary>
		public const string macr = "&#175;";
		/// <summary>degree sign: &#176;</summary>
		public const string deg = "&#176;";
		/// <summary>plus-minus sign = plus-or-minus sign: &#177;</summary>
		public const string plusmn = "&#177;";
		/// <summary>superscript two = superscript digit two = squared: &#178;</summary>
		public const string sup2 = "&#178;";
		/// <summary>superscript three = superscript digit three = cubed: &#179;</summary>
		public const string sup3 = "&#179;";
		/// <summary>acute accent = spacing acute: &#180;</summary>
		public const string acute = "&#180;";
		/// <summary>micro sign: &#181;</summary>
		public const string micro = "&#181;";
		/// <summary>pilcrow sign = paragraph sign: &#182;</summary>
		public const string para = "&#182;";
		/// <summary>middle dot = Georgian comma = Greek middle dot: &#183;</summary>
		public const string middot = "&#183;";
		/// <summary>cedilla = spacing cedilla: &#184;</summary>
		public const string cedil = "&#184;";
		/// <summary>superscript one = superscript digit one: &#185;</summary>
		public const string sup1 = "&#185;";
		/// <summary>masculine ordinal indicator: &#186;</summary>
		public const string ordm = "&#186;";
		/// <summary>right-pointing double angle quotation mark = right pointing guillemet: &#187;</summary>
		public const string raquo = "&#187;";
		/// <summary>vulgar fraction one quarter = fraction one quarter: &#188;</summary>
		public const string frac14 = "&#188;";
		/// <summary>vulgar fraction one half = fraction one half: &#189;</summary>
		public const string frac12 = "&#189;";
		/// <summary>vulgar fraction three quarters = fraction three quarters: &#190;</summary>
		public const string frac34 = "&#190;";
		/// <summary>inverted question mark = turned question mark: &#191;</summary>
		public const string iquest = "&#191;";
		/// <summary>latin capital letter A with grave = latin capital letter A grave: &#192;</summary>
		public const string Agrave = "&#192;";
		/// <summary>latin capital letter A with acute: &#193;</summary>
		public const string Aacute = "&#193;";
		/// <summary>latin capital letter A with circumflex: &#194;</summary>
		public const string Acirc = "&#194;";
		/// <summary>latin capital letter A with tilde: &#195;</summary>
		public const string Atilde = "&#195;";
		/// <summary>latin capital letter A with diaeresis: &#196;</summary>
		public const string Auml = "&#196;";
		/// <summary>latin capital letter A with ring above = latin capital letter A ring: &#197;</summary>
		public const string Aring = "&#197;";
		/// <summary>latin capital letter AE = latin capital ligature AE: &#198;</summary>
		public const string AElig = "&#198;";
		/// <summary>latin capital letter C with cedilla: &#199;</summary>
		public const string Ccedil = "&#199;";
		/// <summary>latin capital letter E with grave: &#200;</summary>
		public const string Egrave = "&#200;";
		/// <summary>latin capital letter E with acute: &#201;</summary>
		public const string Eacute = "&#201;";
		/// <summary>latin capital letter E with circumflex: &#202;</summary>
		public const string Ecirc = "&#202;";
		/// <summary>latin capital letter E with diaeresis: &#203;</summary>
		public const string Euml = "&#203;";
		/// <summary>latin capital letter I with grave: &#204;</summary>
		public const string Igrave = "&#204;";
		/// <summary>latin capital letter I with acute: &#205;</summary>
		public const string Iacute = "&#205;";
		/// <summary>latin capital letter I with circumflex: &#206;</summary>
		public const string Icirc = "&#206;";
		/// <summary>latin capital letter I with diaeresis: &#207;</summary>
		public const string Iuml = "&#207;";
		/// <summary>latin capital letter ETH: &#208;</summary>
		public const string ETH = "&#208;";
		/// <summary>latin capital letter N with tilde: &#209;</summary>
		public const string Ntilde = "&#209;";
		/// <summary>latin capital letter O with grave: &#210;</summary>
		public const string Ograve = "&#210;";
		/// <summary>latin capital letter O with acute: &#211;</summary>
		public const string Oacute = "&#211;";
		/// <summary>latin capital letter O with circumflex: &#212;</summary>
		public const string Ocirc = "&#212;";
		/// <summary>latin capital letter O with tilde: &#213;</summary>
		public const string Otilde = "&#213;";
		/// <summary>latin capital letter O with diaeresis: &#214;</summary>
		public const string Ouml = "&#214;";
		/// <summary>multiplication sign: &#215;</summary>
		public const string times = "&#215;";
		/// <summary>latin capital letter O with stroke = latin capital letter O slash: &#216;</summary>
		public const string Oslash = "&#216;";
		/// <summary>latin capital letter U with grave: &#217;</summary>
		public const string Ugrave = "&#217;";
		/// <summary>latin capital letter U with acute: &#218;</summary>
		public const string Uacute = "&#218;";
		/// <summary>latin capital letter U with circumflex: &#219;</summary>
		public const string Ucirc = "&#219;";
		/// <summary>latin capital letter U with diaeresis: &#220;</summary>
		public const string Uuml = "&#220;";
		/// <summary>latin capital letter Y with acute: &#221;</summary>
		public const string Yacute = "&#221;";
		/// <summary>latin capital letter THORN: &#222;</summary>
		public const string THORN = "&#222;";
		/// <summary>latin small letter sharp s = ess-zed: &#223;</summary>
		public const string szlig = "&#223;";
		/// <summary>latin small letter a with grave = latin small letter a grave: &#224;</summary>
		public const string agrave = "&#224;";
		/// <summary>latin small letter a with acute: &#225;</summary>
		public const string aacute = "&#225;";
		/// <summary>latin small letter a with circumflex: &#226;</summary>
		public const string acirc = "&#226;";
		/// <summary>latin small letter a with tilde: &#227;</summary>
		public const string atilde = "&#227;";
		/// <summary>latin small letter a with diaeresis: &#228;</summary>
		public const string auml = "&#228;";
		/// <summary>latin small letter a with ring above = latin small letter a ring: &#229;</summary>
		public const string aring = "&#229;";
		/// <summary>latin small letter ae = latin small ligature ae: &#230;</summary>
		public const string aelig = "&#230;";
		/// <summary>latin small letter c with cedilla: &#231;</summary>
		public const string ccedil = "&#231;";
		/// <summary>latin small letter e with grave: &#232;</summary>
		public const string egrave = "&#232;";
		/// <summary>latin small letter e with acute: &#233;</summary>
		public const string eacute = "&#233;";
		/// <summary>latin small letter e with circumflex: &#234;</summary>
		public const string ecirc = "&#234;";
		/// <summary>latin small letter e with diaeresis: &#235;</summary>
		public const string euml = "&#235;";
		/// <summary>latin small letter i with grave: &#236;</summary>
		public const string igrave = "&#236;";
		/// <summary>latin small letter i with acute: &#237;</summary>
		public const string iacute = "&#237;";
		/// <summary>latin small letter i with circumflex: &#238;</summary>
		public const string icirc = "&#238;";
		/// <summary>latin small letter i with diaeresis: &#239;</summary>
		public const string iuml = "&#239;";
		/// <summary>latin small letter eth: &#240;</summary>
		public const string eth = "&#240;";
		/// <summary>latin small letter n with tilde: &#241;</summary>
		public const string ntilde = "&#241;";
		/// <summary>latin small letter o with grave: &#242;</summary>
		public const string ograve = "&#242;";
		/// <summary>latin small letter o with acute: &#243;</summary>
		public const string oacute = "&#243;";
		/// <summary>latin small letter o with circumflex: &#244;</summary>
		public const string ocirc = "&#244;";
		/// <summary>latin small letter o with tilde: &#245;</summary>
		public const string otilde = "&#245;";
		/// <summary>latin small letter o with diaeresis: &#246;</summary>
		public const string ouml = "&#246;";
		/// <summary>division sign: &#247;</summary>
		public const string divide = "&#247;";
		/// <summary>latin small letter o with stroke: &#248;</summary>
		public const string oslash = "&#248;";
		/// <summary>latin small letter u with grave: &#249;</summary>
		public const string ugrave = "&#249;";
		/// <summary>latin small letter u with acute: &#250;</summary>
		public const string uacute = "&#250;";
		/// <summary>latin small letter u with circumflex: &#251;</summary>
		public const string ucirc = "&#251;";
		/// <summary>latin small letter u with diaeresis: &#252;</summary>
		public const string uuml = "&#252;";
		/// <summary>latin small letter y with acute: &#253;</summary>
		public const string yacute = "&#253;";
		/// <summary>latin small letter thorn: &#254;</summary>
		public const string thorn = "&#254;";
		/// <summary>latin small letter y with diaeresis: &#255;</summary>
		public const string yuml = "&#255;";
		/// <summary>latin small f with hook = function = florin: &#402;</summary>
		public const string fnof = "&#402;";
		/// <summary>greek capital letter alpha: &#913;</summary>
		public const string Alpha = "&#913;";
		/// <summary>greek capital letter beta: &#914;</summary>
		public const string Beta = "&#914;";
		/// <summary>greek capital letter gamma: &#915;</summary>
		public const string Gamma = "&#915;";
		/// <summary>greek capital letter delta: &#916;</summary>
		public const string Delta = "&#916;";
		/// <summary>greek capital letter epsilon: &#917;</summary>
		public const string Epsilon = "&#917;";
		/// <summary>greek capital letter zeta: &#918;</summary>
		public const string Zeta = "&#918;";
		/// <summary>greek capital letter eta: &#919;</summary>
		public const string Eta = "&#919;";
		/// <summary>greek capital letter theta: &#920;</summary>
		public const string Theta = "&#920;";
		/// <summary>greek capital letter iota: &#921;</summary>
		public const string Iota = "&#921;";
		/// <summary>greek capital letter kappa: &#922;</summary>
		public const string Kappa = "&#922;";
		/// <summary>greek capital letter lambda: &#923;</summary>
		public const string Lambda = "&#923;";
		/// <summary>greek capital letter mu: &#924;</summary>
		public const string Mu = "&#924;";
		/// <summary>greek capital letter nu: &#925;</summary>
		public const string Nu = "&#925;";
		/// <summary>greek capital letter xi: &#926;</summary>
		public const string Xi = "&#926;";
		/// <summary>greek capital letter omicron: &#927;</summary>
		public const string Omicron = "&#927;";
		/// <summary>greek capital letter pi: &#928;</summary>
		public const string Pi = "&#928;";
		/// <summary>greek capital letter rho: &#929;</summary>
		public const string Rho = "&#929;";
		/// <summary>greek capital letter sigma: &#931;</summary>
		public const string Sigma = "&#931;";
		/// <summary>greek capital letter tau: &#932;</summary>
		public const string Tau = "&#932;";
		/// <summary>greek capital letter upsilon: &#933;</summary>
		public const string Upsilon = "&#933;";
		/// <summary>greek capital letter phi: &#934;</summary>
		public const string Phi = "&#934;";
		/// <summary>greek capital letter chi: &#935;</summary>
		public const string Chi = "&#935;";
		/// <summary>greek capital letter psi: &#936;</summary>
		public const string Psi = "&#936;";
		/// <summary>greek capital letter omega: &#937;</summary>
		public const string Omega = "&#937;";
		/// <summary>greek small letter alpha: &#945;</summary>
		public const string alpha = "&#945;";
		/// <summary>greek small letter beta: &#946;</summary>
		public const string beta = "&#946;";
		/// <summary>greek small letter gamma: &#947;</summary>
		public const string gamma = "&#947;";
		/// <summary>greek small letter delta: &#948;</summary>
		public const string delta = "&#948;";
		/// <summary>greek small letter epsilon: &#949;</summary>
		public const string epsilon = "&#949;";
		/// <summary>greek small letter zeta: &#950;</summary>
		public const string zeta = "&#950;";
		/// <summary>greek small letter eta: &#951;</summary>
		public const string eta = "&#951;";
		/// <summary>greek small letter theta: &#952;</summary>
		public const string theta = "&#952;";
		/// <summary>greek small letter iota: &#953;</summary>
		public const string iota = "&#953;";
		/// <summary>greek small letter kappa: &#954;</summary>
		public const string kappa = "&#954;";
		/// <summary>greek small letter lambda: &#955;</summary>
		public const string lambda = "&#955;";
		/// <summary>greek small letter mu: &#956;</summary>
		public const string mu = "&#956;";
		/// <summary>greek small letter nu: &#957;</summary>
		public const string nu = "&#957;";
		/// <summary>greek small letter xi: &#958;</summary>
		public const string xi = "&#958;";
		/// <summary>greek small letter omicron: &#959;</summary>
		public const string omicron = "&#959;";
		/// <summary>greek small letter pi: &#960;</summary>
		public const string pi = "&#960;";
		/// <summary>greek small letter rho: &#961;</summary>
		public const string rho = "&#961;";
		/// <summary>greek small letter final sigma: &#962;</summary>
		public const string sigmaf = "&#962;";
		/// <summary>greek small letter sigma: &#963;</summary>
		public const string sigma = "&#963;";
		/// <summary>greek small letter tau: &#964;</summary>
		public const string tau = "&#964;";
		/// <summary>greek small letter upsilon: &#965;</summary>
		public const string upsilon = "&#965;";
		/// <summary>greek small letter phi: &#966;</summary>
		public const string phi = "&#966;";
		/// <summary>greek small letter chi: &#967;</summary>
		public const string chi = "&#967;";
		/// <summary>greek small letter psi: &#968;</summary>
		public const string psi = "&#968;";
		/// <summary>greek small letter omega: &#969;</summary>
		public const string omega = "&#969;";
		/// <summary>greek small letter theta symbol: &#977;</summary>
		public const string thetasym = "&#977;";
		/// <summary>greek upsilon with hook symbol: &#978;</summary>
		public const string upsih = "&#978;";
		/// <summary>greek pi symbol: &#982;</summary>
		public const string piv = "&#982;";
		/// <summary>bullet = black small circle: &#8226;</summary>
		public const string bull = "&#8226;";
		/// <summary>horizontal ellipsis = three dot leader: &#8230;</summary>
		public const string hellip = "&#8230;";
		/// <summary>prime = minutes = feet: &#8242;</summary>
		public const string prime = "&#8242;";
		/// <summary>double prime = seconds = inches: &#8243;</summary>
		public const string Prime = "&#8243;";
		/// <summary>overline = spacing overscore: &#8254;</summary>
		public const string oline = "&#8254;";
		/// <summary>fraction slash: &#8260;</summary>
		public const string frasl = "&#8260;";
		/// <summary>script capital P = power set = Weierstrass p: &#8472;</summary>
		public const string weierp = "&#8472;";
		/// <summary>blackletter capital I = imaginary part: &#8465;</summary>
		public const string image = "&#8465;";
		/// <summary>blackletter capital R = real part symbol: &#8476;</summary>
		public const string real = "&#8476;";
		/// <summary>trade mark sign: &#8482;</summary>
		public const string trade = "&#8482;";
		/// <summary>alef symbol = first transfinite cardinal: &#8501;</summary>
		public const string alefsym = "&#8501;";
		/// <summary>leftwards arrow: &#8592;</summary>
		public const string larr = "&#8592;";
		/// <summary>upwards arrow: &#8593;</summary>
		public const string uarr = "&#8593;";
		/// <summary>rightwards arrow: &#8594;</summary>
		public const string rarr = "&#8594;";
		/// <summary>downwards arrow: &#8595;</summary>
		public const string darr = "&#8595;";
		/// <summary>left right arrow: &#8596;</summary>
		public const string harr = "&#8596;";
		/// <summary>downwards arrow with corner leftwards = carriage return: &#8629;</summary>
		public const string crarr = "&#8629;";
		/// <summary>leftwards double arrow: &#8656;</summary>
		public const string lArr = "&#8656;";
		/// <summary>upwards double arrow: &#8657;</summary>
		public const string uArr = "&#8657;";
		/// <summary>rightwards double arrow: &#8658;</summary>
		public const string rArr = "&#8658;";
		/// <summary>downwards double arrow: &#8659;</summary>
		public const string dArr = "&#8659;";
		/// <summary>left right double arrow: &#8660;</summary>
		public const string hArr = "&#8660;";
		/// <summary>for all: &#8704;</summary>
		public const string forall = "&#8704;";
		/// <summary>partial differential: &#8706;</summary>
		public const string part = "&#8706;";
		/// <summary>there exists: &#8707;</summary>
		public const string exist = "&#8707;";
		/// <summary>empty set = null set = diameter: &#8709;</summary>
		public const string empty = "&#8709;";
		/// <summary>nabla = backward difference: &#8711;</summary>
		public const string nabla = "&#8711;";
		/// <summary>element of: &#8712;</summary>
		public const string isin = "&#8712;";
		/// <summary>not an element of: &#8713;</summary>
		public const string notin = "&#8713;";
		/// <summary>contains as member: &#8715;</summary>
		public const string ni = "&#8715;";
		/// <summary>n-ary product = product sign: &#8719;</summary>
		public const string prod = "&#8719;";
		/// <summary>n-ary sumation: &#8721;</summary>
		public const string sum = "&#8721;";
		/// <summary>minus sign: &#8722;</summary>
		public const string minus = "&#8722;";
		/// <summary>asterisk operator: &#8727;</summary>
		public const string lowast = "&#8727;";
		/// <summary>square root = radical sign: &#8730;</summary>
		public const string radic = "&#8730;";
		/// <summary>proportional to: &#8733;</summary>
		public const string prop = "&#8733;";
		/// <summary>infinity: &#8734;</summary>
		public const string infin = "&#8734;";
		/// <summary>angle: &#8736;</summary>
		public const string ang = "&#8736;";
		/// <summary>logical and = wedge: &#8743;</summary>
		public const string and = "&#8743;";
		/// <summary>logical or = vee: &#8744;</summary>
		public const string or = "&#8744;";
		/// <summary>intersection = cap: &#8745;</summary>
		public const string cap = "&#8745;";
		/// <summary>union = cup: &#8746;</summary>
		public const string cup = "&#8746;";
		/// <summary>integral: &#8747;</summary>
		public const string @int = "&#8747;";
		/// <summary>therefore: &#8756;</summary>
		public const string there4 = "&#8756;";
		/// <summary>tilde operator = varies with = similar to: &#8764;</summary>
		public const string sim = "&#8764;";
		/// <summary>approximately equal to: &#8773;</summary>
		public const string cong = "&#8773;";
		/// <summary>almost equal to = asymptotic to: &#8776;</summary>
		public const string asymp = "&#8776;";
		/// <summary>not equal to: &#8800;</summary>
		public const string ne = "&#8800;";
		/// <summary>identical to: &#8801;</summary>
		public const string equiv = "&#8801;";
		/// <summary>less-than or equal to: &#8804;</summary>
		public const string le = "&#8804;";
		/// <summary>greater-than or equal to: &#8805;</summary>
		public const string ge = "&#8805;";
		/// <summary>subset of: &#8834;</summary>
		public const string sub = "&#8834;";
		/// <summary>superset of: &#8835;</summary>
		public const string sup = "&#8835;";
		/// <summary>not a subset of: &#8836;</summary>
		public const string nsub = "&#8836;";
		/// <summary>subset of or equal to: &#8838;</summary>
		public const string sube = "&#8838;";
		/// <summary>superset of or equal to: &#8839;</summary>
		public const string supe = "&#8839;";
		/// <summary>circled plus = direct sum: &#8853;</summary>
		public const string oplus = "&#8853;";
		/// <summary>circled times = vector product: &#8855;</summary>
		public const string otimes = "&#8855;";
		/// <summary>up tack = orthogonal to = perpendicular: &#8869;</summary>
		public const string perp = "&#8869;";
		/// <summary>dot operator: &#8901;</summary>
		public const string sdot = "&#8901;";
		/// <summary>left ceiling = apl upstile: &#8968;</summary>
		public const string lceil = "&#8968;";
		/// <summary>right ceiling: &#8969;</summary>
		public const string rceil = "&#8969;";
		/// <summary>left floor = apl downstile: &#8970;</summary>
		public const string lfloor = "&#8970;";
		/// <summary>right floor: &#8971;</summary>
		public const string rfloor = "&#8971;";
		/// <summary>left-pointing angle bracket = bra: &#9001;</summary>
		public const string lang = "&#9001;";
		/// <summary>right-pointing angle bracket = ket: &#9002;</summary>
		public const string rang = "&#9002;";
		/// <summary>lozenge: &#9674;</summary>
		public const string loz = "&#9674;";
		/// <summary>black spade suit: &#9824;</summary>
		public const string spades = "&#9824;";
		/// <summary>black club suit = shamrock: &#9827;</summary>
		public const string clubs = "&#9827;";
		/// <summary>black heart suit = valentine: &#9829;</summary>
		public const string hearts = "&#9829;";
		/// <summary>black diamond suit: &#9830;</summary>
		public const string diams = "&#9830;";
		/// <summary>quotation mark = APL quote: &#34;</summary>
		public const string quot = "&#34;";
		/// <summary>ampersand: &#38;</summary>
		public const string amp = "&#38;";
		/// <summary>less-than sign: &#60;</summary>
		public const string lt = "&#60;";
		/// <summary>greater-than sign: &#62;</summary>
		public const string gt = "&#62;";
		/// <summary>latin capital ligature OE: &#338;</summary>
		public const string OElig = "&#338;";
		/// <summary>latin small ligature oe: &#339;</summary>
		public const string oelig = "&#339;";
		/// <summary>latin capital letter S with caron: &#352;</summary>
		public const string Scaron = "&#352;";
		/// <summary>latin small letter s with caron: &#353;</summary>
		public const string scaron = "&#353;";
		/// <summary>latin capital letter Y with diaeresis: &#376;</summary>
		public const string Yuml = "&#376;";
		/// <summary>modifier letter circumflex accent: &#710;</summary>
		public const string circ = "&#710;";
		/// <summary>small tilde: &#732;</summary>
		public const string tilde = "&#732;";
		/// <summary>en space: &#8194;</summary>
		public const string ensp = "&#8194;";
		/// <summary>em space: &#8195;</summary>
		public const string emsp = "&#8195;";
		/// <summary>thin space: &#8201;</summary>
		public const string thinsp = "&#8201;";
		/// <summary>zero width non-joiner: &#8204;</summary>
		public const string zwnj = "&#8204;";
		/// <summary>zero width joiner: &#8205;</summary>
		public const string zwj = "&#8205;";
		/// <summary>left-to-right mark: &#8206;</summary>
		public const string lrm = "&#8206;";
		/// <summary>right-to-left mark: &#8207;</summary>
		public const string rlm = "&#8207;";
		/// <summary>en dash: &#8211;</summary>
		public const string ndash = "&#8211;";
		/// <summary>em dash: &#8212;</summary>
		public const string mdash = "&#8212;";
		/// <summary>left single quotation mark: &#8216;</summary>
		public const string lsquo = "&#8216;";
		/// <summary>right single quotation mark: &#8217;</summary>
		public const string rsquo = "&#8217;";
		/// <summary>single low-9 quotation mark: &#8218;</summary>
		public const string sbquo = "&#8218;";
		/// <summary>left double quotation mark: &#8220;</summary>
		public const string ldquo = "&#8220;";
		/// <summary>right double quotation mark: &#8221;</summary>
		public const string rdquo = "&#8221;";
		/// <summary>double low-9 quotation mark: &#8222;</summary>
		public const string bdquo = "&#8222;";
		/// <summary>dagger: &#8224;</summary>
		public const string dagger = "&#8224;";
		/// <summary>double dagger: &#8225;</summary>
		public const string Dagger = "&#8225;";
		/// <summary>per mille sign: &#8240;</summary>
		public const string permil = "&#8240;";
		/// <summary>single left-pointing angle quotation mark: &#8249;</summary>
		public const string lsaquo = "&#8249;";
		/// <summary>single right-pointing angle quotation mark: &#8250;</summary>
		public const string rsaquo = "&#8250;";
		/// <summary>euro sign: &#8364;</summary>
		public const string euro = "&#8364;";

	}
}
