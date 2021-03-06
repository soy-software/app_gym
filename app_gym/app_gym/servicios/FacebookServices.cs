﻿using app_gym.modelos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace app_gym.servicios
{
    public class FacebookServices
    {
        private ResFull _res = new ResFull();
        public async Task<FacebookProfile> GetFacebookProfileAsync(string accessToken)
        {


            var requestUrl =
                 "https://graph.facebook.com/v5.0/me/?fields=name,picture,work,website,religion,location,locale,link,cover,age_range,birthday,devices,email,first_name,last_name,gender,hometown,is_verified,languages&access_token="
                 + accessToken;

            var httpClient = new HttpClient();

            var userJson = await httpClient.GetStringAsync(requestUrl);

            var facebookProfile = JsonConvert.DeserializeObject<FacebookProfile>(userJson);

            return facebookProfile;


        }
    }
}
