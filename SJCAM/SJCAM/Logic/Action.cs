﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SJCAM.Logic
{
	public class Action
	{
		private const string baseURL = "http://192.168.1.254/";
		private const string basePhotoURL = "http://192.168.1.254/DCIM/PHOTO";
		private const string baseVideoURL = "http://192.168.1.254/DCIM/MOVIE";
		private const string LiveFeedURL = "rtsp://192.168.1.254/sjcam.mp4";
		public string LiveFeed { get { return LiveFeedURL; } }
	
		bool DEBUG = false;

		public Action()
		{

		}

		private string BuildRequestAsync(string cmdNumber, string param = "")
		{
			string tmp = baseURL + "?custom=1&cmd=" + cmdNumber;
			if (param != "")
				tmp += "&par=" + param;
			return tmp;
		}

		public async Task<string> FileRequestAsync(string FileName)
		{
			if (ConnectionStatus.IsConnected != true)
				return null;

			string tmp = baseURL + FileName;
			HttpClient client = new HttpClient();
			try
			{
				string msg = await client.GetStringAsync(tmp);
				Debug.WriteLineIf(DEBUG, msg);
				return msg;
			}
			catch (Exception e)
			{
				Debug.WriteLine("[FILE REQUEST]: get error to get file");
                e.ToString();
                return null;
			}
		}

		public async Task<string> GetRequestAsync(string cmdNumber, string param = "")
		{
			if (ConnectionStatus.IsConnected != true)
				return null;

			string tmp = BuildRequestAsync(cmdNumber, param);
			HttpClient client = new HttpClient();
			try
			{
				string msg = await client.GetStringAsync(tmp);
				Debug.WriteLineIf(DEBUG, msg);
				return msg;
			}
			catch (Exception e)
			{
				Debug.WriteLine("[REQUEST]: get error to get request");
                e.ToString();
				return null;
			}
		}
	}
}
