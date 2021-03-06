﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.Serialization.RH
{
	public abstract class CacheSynchronizer<T> where T : new()
	{
		protected CacheFile cacheFile;

		public CacheSynchronizer(CacheFile cacheFile)
		{
			this.cacheFile = cacheFile;
		}

		public void Sync()
		{
			var existingEntry = FindEntry();
			if (existingEntry != null)
				Update(existingEntry);
			else
			{
				var newEntry = Create();
				AddEntry(newEntry);
			}
		}

		protected abstract T FindEntry();

		protected abstract void AddEntry(T entry);

		protected T Create()
		{
			var entry = new T();

			Update(entry);

			return entry;
		}

		protected abstract void Update(T entry);
	}
}
