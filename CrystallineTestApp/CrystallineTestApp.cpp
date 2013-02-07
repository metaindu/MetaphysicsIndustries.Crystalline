// CrystallineTestApp.cpp : main project file.

#include "stdafx.h"
#include "CrystallineTestAppForm.h"

using namespace CrystallineTestApp;

[STAThreadAttribute]
int main(array<System::String ^> ^args)
{
	// Enabling Windows XP visual effects before any controls are created
	Application::EnableVisualStyles();
	Application::SetCompatibleTextRenderingDefault(false); 

	// Create the main window and run it
	Form^	f;
	f = gcnew CrystallineTestAppForm;
	Application::Run(f);
	return 0;
}
