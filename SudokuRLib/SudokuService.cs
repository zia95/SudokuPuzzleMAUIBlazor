using Microsoft.JSInterop;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SudokuRLib;

public class SudokuService
{
	private readonly JSBinder _jsBinder;

	public SudokuService(IJSRuntime JSRuntime)
	{
		_jsBinder = new JSBinder(JSRuntime, "./_content/SudokuRLib/js/qqwing-1.3.4.js");
	}

	public async Task<SudokuPuzzle[]> GenerateAsync(int count)
	{
		try
		{
			var module = await _jsBinder.GetModule();

			var res = await module.InvokeAsync<SudokuPuzzle[]>("generate", count);


			return res;
		}
		catch(Exception ex)
		{
			string s = ex.Message;
			throw;
		}
		
	}
}
