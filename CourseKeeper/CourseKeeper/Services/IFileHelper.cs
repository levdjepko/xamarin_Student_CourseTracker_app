using System;
namespace CourseKeeper.Services
{
	public interface IFileHelper
	{
		string GetLocalFilePath(string filename);
	}
}
