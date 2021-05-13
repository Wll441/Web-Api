using JWT;
using JWT.Algorithms;
using JWT.Exceptions;
using JWT.Serializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web_Api.Tools
{
    public class JwtTools
    {
        public string Secret { get; set; } = "asldfkjlksdfjwkerjwkeDFDSdfrjlskfj";
        //加密
        public string JwtEncode(Dictionary<string,object> pairs)
        {
            IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
            IJsonSerializer serializer = new JsonNetSerializer();
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);
            return encoder.Encode(pairs, Secret);
        }
        //解密
        public string JwtDecode(string token)
        {
            try
            {
                IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
                IJsonSerializer serializer = new JsonNetSerializer();
                IDateTimeProvider provider = new UtcDateTimeProvider();
                IJwtValidator validator = new JwtValidator(serializer, provider);
                IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
                IJwtDecoder decoder = new JwtDecoder(serializer, validator, urlEncoder, algorithm);


                return decoder.Decode(token, Secret, verify: true);
            }
            catch (TokenExpiredException)
            {
                throw new Exception("Token has expired");

            }
            catch (SignatureVerificationException)
            {
                throw new Exception("Token has invalid signature");
            }
        }
    }
}