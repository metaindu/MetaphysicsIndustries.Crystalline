#include "StdAfx.h"
#include "CrystallineTestAppForm.h"

using namespace MetaphysicsIndustries::Crystalline;
using namespace MetaphysicsIndustries::Collections;


NAMESPACE_START;



void CrystallineTestAppForm::CrystallineTestAppForm_Load(Object^, EventArgs^)
{

	BoxFramework^	f;
	Box^			b;

	f = gcnew BoxFramework();

	//b = gcnew Box();
	//b->Rect = Rectangle(25, 25, 50, 50);
	//f->Add(b);

	//b = gcnew Box();
	//b->Rect = Rectangle(85, 20, 50, 30);
	//f->Add(b);

	//b = gcnew Box();
	//b->Rect = Rectangle(150, 10, 50, 80);
	//f->Add(b);

	//b = gcnew Box();
	//b->Rect = Rectangle(225, 45, 50, 40);
	//f->Add(b);

	//b = gcnew Box();
	//b->Rect = Rectangle(325, 25, 50, 50);
	//f->Add(b);



	this->crystal = gcnew MetaphysicsIndustries::Crystalline::CrystallineControl;

	this->toolStripContainer1->ContentPanel->SuspendLayout();
	this->toolStripContainer1->SuspendLayout();

	this->toolStripContainer1->ContentPanel->Controls->Add(this->crystal);

	this->crystal->Dock = System::Windows::Forms::DockStyle::Fill;
	this->crystal->Location = System::Drawing::Point(0, 0);
	this->crystal->Name = L"crystal";
	this->crystal->Size = System::Drawing::Size(292, 227);
	this->crystal->TabIndex = 0;

	this->toolStripContainer1->ContentPanel->ResumeLayout(false);
	this->toolStripContainer1->ResumeLayout(false);
	this->toolStripContainer1->PerformLayout();


	Element^	e1;
	Element^	e2;
	Path^		p;

	//e1 = gcnew Element;
	//e1->Location = PointF(50, 50);
	//e1->Size = SizeF(50, 20);

	//e2 = gcnew Element;
	//e2->Location = PointF(150, 100);
	//e2->Size = SizeF(50, 20);

	//p = gcnew Path;
	//p->From = e1;
	//p->To = e2;
	//p->PathJoints->Add(gcnew PathJoint(PointF(100,  60)));
	//p->PathJoints->Add(gcnew PathJoint(PointF(125,  60)));
	//p->PathJoints->Add(gcnew PathJoint(PointF(125, 110)));
	//p->PathJoints->Add(gcnew PathJoint(PointF(150, 110)));

	//this->crystal->Elements->Add(e1);
	//this->crystal->Elements->Add(e2);
	//this->crystal->Paths->Add(p);

	//array<String^>^	names = { "Clive", "Tom", "Frederick", "Jaques", "Larry", "Clarence" };
	for each (IBox^ ib in f)
	{
		e1 = gcnew Element();
		e1->Location = ib->Location;
		e1->Size = ib->Size;
		//e1->Param = names[this->crystal->Elements->Count];
		this->crystal->Elements->Add(e1);
	}

	//Set<IBox^>^	boxes;
	//boxes = f->GetNeighborsRightward(0, 0, 100);

	//for each (IBox^ ib in boxes)
	//{
	//	e1 = gcnew Element();
	//	e1->Location = ib->Location + Drawing::Size(2,2);
	//	e1->Size = ib->Size;
	//	this->crystal->Elements->Add(e1);
	//}

	//for each (IBox^ ib in this->crystal->Elements)
	//{
	//	this->crystal->Framework->Add(ib);
	//}
}



NAMESPACE_END



