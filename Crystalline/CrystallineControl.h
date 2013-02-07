#pragma once

using namespace System;
using namespace System::ComponentModel;
using namespace System::Collections::Generic;
using namespace System::Windows::Forms;
using namespace System::Data;
using namespace System::Drawing;
using namespace System::Windows;
using namespace MetaphysicsIndustries::Collections;

#include "Element.h"
#include "Path.h"
#include "PathJoint.h"
#include "PathingJunction.h"
#include "Pathway.h"
#include "BoxFramework.h"
#include "ElementCollection.h"
#include "PathwayCollection.h"
#include "PathingJunctionCollection.h"

namespace MetaphysicsIndustries { namespace Crystalline {

	/// <summary>
	/// Summary for CrystallineControl
	/// </summary>
	public ref class CrystallineControl : public System::Windows::Forms::UserControl
	{
	public:
		CrystallineControl(void)
		{
			InitializeComponent();
			//
			//TODO: Add the constructor code here
			//
			this->framework = gcnew BoxFramework();

			this->elements = gcnew ElementCollection(this->framework);
			this->paths = gcnew Set<Path^>;
			this->pathingjunctions = gcnew PathingJunctionCollection(this->framework);
			this->pathways = gcnew PathwayCollection(this->framework);//Set<Pathway^>();

			this->selectionelement = gcnew Set<Element^>;
			this->selectionpathjoint = gcnew Set<PathJoint^>;
			this->selectionpathingjunction = gcnew Set<PathingJunction^>();
			this->selectionpathway = gcnew Set<Pathway^>();

			//this->selectingelement = true;
			this->SelectionMode = SelectionModeType::Element;

			this->BackColor = Color::White;
			this->BorderStyle = Windows::Forms::BorderStyle::Fixed3D;
		}

	protected:
		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		~CrystallineControl()
		{
			if (components)
			{
				delete components;
			}
		}

	private:
		/// <summary>
		/// Required designer variable.
		/// </summary>
		System::ComponentModel::Container ^components;

#pragma region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		void InitializeComponent(void)
		{
			this->SuspendLayout();
			// 
			// CrystallineControl
			// 
			this->AutoScaleDimensions = System::Drawing::SizeF(6, 13);
			this->AutoScaleMode = System::Windows::Forms::AutoScaleMode::Font;
			this->Name = L"CrystallineControl";
			this->Load += gcnew System::EventHandler(this, &CrystallineControl::CrystallineControl_Load);
			this->ResumeLayout(false);

		}
#pragma endregion

	public:

		//these will have to be changed to coordinate with BoxFramework

		virtual property ElementCollection^ Elements
		{
			ElementCollection^ get(void);
		}
		virtual property Set<Path^>^ Paths
		{
			Set<Path^>^ get(void);
		}
		virtual property PathingJunctionCollection^ PathingJunctions
		{
			PathingJunctionCollection^ get(void);
		}
		virtual property PathwayCollection^ Pathways
		{
			PathwayCollection^ get(void);
		}

		//eeeeewwwww
		//virtual property BoxFramework^ Framework
		//{
		//	BoxFramework^ get(void);
		//}

	protected:

		ref class CleanupComparer : IComparer<Element^>
		{
		public:
			CleanupComparer(bool SortByY);
			virtual int Compare(Element^, Element^);
		protected:
			bool	sortbyy;
		};

		//ref class ElementComparer : IComparer<Element^>
		//{
		//public:
		//	ElementComparer(bool LeftTopVsRightBottom, bool Vertical);
		//	virtual int Compare(Element^, Element^);
		//protected:
		//	bool	lefttopvsrightbottom;
		//	bool	vertical;
		//};

		enum class SelectionModeType
		{
			Element,
			Path,
			PathingJunction,
			Pathway,
		};

		virtual void SetupContextMenu(void);

		virtual void UpdateContextMenu(void);

		virtual Element^ CreateElement(void);
		virtual Path^ CreatePath(void);
		virtual PathingJunction^ CreatePathingJunction(void);
		virtual Pathway^ CreatePathway(void);

		virtual Set<Element^>^ GetElementsAtPoint(PointF);
		virtual Set<Element^>^ GetElementsInRect(RectangleF);
		virtual Set<PathJoint^>^ GetPathJointsAtPoint(PointF);
		virtual Set<PathJoint^>^ GetPathJointsInRect(RectangleF);
		virtual Set<PathingJunction^>^ GetPathingJunctionsAtPoint(PointF);
		virtual Set<PathingJunction^>^ GetPathingJunctionsInRect(RectangleF);
		virtual Set<Pathway^>^ GetPathwaysAtPoint(PointF);
		virtual Set<Pathway^>^ GetPathwaysInRect(RectangleF);

		virtual void RoutePath(Path^ p);
		//virtual void Cleanup(void);
		//virtual void CleanupElements(void);
		//virtual void CleanupPaths(void);
		//virtual void Cleanup3(void);

		virtual void ConnectInboundPath(Path^, Element^, PointF);
		virtual void ConnectOutboundPath(Path^, Element^, PointF);

		//virtual void SortElements(void);

		virtual property SelectionModeType SelectionMode
		{
			SelectionModeType get(void);
			void set(SelectionModeType);
		}

		BoxFramework^				framework;

		ElementCollection^			elements;
		Set<Path^>^					paths;
		PathingJunctionCollection^	pathingjunctions;
		PathwayCollection^			pathways;

		//List<Element^>^				SortElementsLeft;
		//List<Element^>^				SortElementsTop;
		//List<Element^>^				SortElementsRight;
		//List<Element^>^				SortElementsBottom;
		//ElementComparer^			SorterLeft;
		//ElementComparer^			SorterTop;
		//ElementComparer^			SorterRight;
		//ElementComparer^			SorterBottom;

		ToolStripMenuItem^			CreateElementItem;
		ToolStripMenuItem^			CreatePathItem;
		ToolStripMenuItem^			DeleteItem;
		ToolStripMenuItem^			SelectElementItem;
		ToolStripMenuItem^			SelectPathItem;
		ToolStripMenuItem^			CleanupItem;

		ToolStripMenuItem^			CreatePathingJunctionItem;
		ToolStripMenuItem^			SelectPathingJunctionItem;

		ToolStripMenuItem^			CreatePathwayItem;
		ToolStripMenuItem^			SelectPathwayItem;

		ToolStripMenuItem^			AddPathwayItem;
		ToolStripMenuItem^			AddPathwayLeftItem;
		ToolStripMenuItem^			AddPathwayRightItem;
		ToolStripMenuItem^			AddPathwayUpItem;
		ToolStripMenuItem^			AddPathwayDownItem;

		//ToolStripMenuItem^

		Point						lastrightclick;

		Set<Element^>^				selectionelement;
		Set<PathJoint^>^			selectionpathjoint;
		Set<PathingJunction^>^		selectionpathingjunction;
		Set<Pathway^>^				selectionpathway;
		//bool						selectingelement;
		SelectionModeType			selectionmode;
		Set<IBox^>^					selectionbox;

		bool						isclick;
		Point						draganchor;
		bool						isdragselecting;
		Point						selectionboxpoint;

		Pen^						selectionpen;
		Drawing::Font^				courierfont;

		List<String^>^				names;

	protected:

		virtual void CreateElementItem_Click(Object^, EventArgs^);
		virtual void CreatePathItem_Click(Object^, EventArgs^);
		virtual void DeleteItem_Click(Object^, EventArgs^);
		virtual void SelectElementItem_Click(Object^, EventArgs^);
		virtual void SelectPathItem_Click(Object^, EventArgs^);
		virtual void CleanupItem_Click(Object^, EventArgs^);

		virtual void CreatePathingJunctionItem_Click(Object^, EventArgs^);
		virtual void SelectPathingJunctionItem_Click(Object^, EventArgs^);
		virtual void CreatePathwayItem_Click(Object^, EventArgs^);
		virtual void SelectPathwayItem_Click(Object^, EventArgs^);

		virtual void AddPathwayLeftItem_Click(Object^, EventArgs^);
		virtual void AddPathwayRightItem_Click(Object^, EventArgs^);
		virtual void AddPathwayUpItem_Click(Object^, EventArgs^);
		virtual void AddPathwayDownItem_Click(Object^, EventArgs^);


		virtual void CrystallineControl_MouseDown(Object^, MouseEventArgs^);
		virtual void CrystallineControl_MouseMove(Object^, MouseEventArgs^);
		virtual void CrystallineControl_MouseUp(Object^, MouseEventArgs^);

		virtual void CrystallineControl_Paint(Object^, PaintEventArgs^);

	private:

		double Dist(PointF p1, PointF p2);

		void CrystallineControl_Load(Object^, EventArgs^);

	};

} }
