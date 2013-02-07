#include "StdAfx.h"
#include "CrystallineControl.h"
#include "InboundToElementPathChildrenCollection.h"
#include "OutboundFromElementPathChildrenCollection.h"

using namespace System::Windows;



NAMESPACE_START;



System::Void CrystallineControl::CrystallineControl_Load(System::Object^  sender, System::EventArgs^  e)
{
	this->SetupContextMenu();

	this->selectionpen = gcnew Pen(Color::FromArgb(191, 191, 255), 2);
	this->courierfont = gcnew System::Drawing::Font("courier new", 8);

	this->names = gcnew List<String^>();

	this->names->Add("Clive");
	this->names->Add("Thomas");
	this->names->Add("Frederick");
	this->names->Add("Jaques");
	this->names->Add("Larry");
	this->names->Add("Clarence");
	this->names->Add("Robert");
	this->names->Add("Andrew");
	this->names->Add("Jeremy");

	this->Paint += gcnew PaintEventHandler(this, &CrystallineControl::CrystallineControl_Paint);

	this->MouseDown += gcnew MouseEventHandler(this, &CrystallineControl::CrystallineControl_MouseDown);
	this->MouseMove += gcnew MouseEventHandler(this, &CrystallineControl::CrystallineControl_MouseMove);
	this->MouseUp += gcnew MouseEventHandler(this, &CrystallineControl::CrystallineControl_MouseUp);
}

void CrystallineControl::CrystallineControl_Paint(Object^, PaintEventArgs^ e)
{
	PaintEventArgs^	_e = e;

	Graphics^	g;

	g = e->Graphics;

	if (true)
	{
		System::Text::StringBuilder^	sb = gcnew System::Text::StringBuilder();

		sb->Append("elements = ");
		sb->Append(this->elements->Count);
		sb->Append("\r\n");
		sb->Append("paths = ");
		sb->Append(this->paths->Count);
		sb->Append("\r\n");
		sb->Append("pathingjunctions = ");
		sb->Append(this->pathingjunctions->Count);
		sb->Append("\r\n");
		sb->Append("pathways = ");
		sb->Append(this->pathways->Count);
		sb->Append("\r\n");
		sb->Append("\r\n");
		//sb->Append("SortElementsLeft = ");
		//sb->Append(this->SortElementsLeft->Count);
		//sb->Append("\r\n");
		//sb->Append("SortElementsRight = ");
		//sb->Append(this->SortElementsRight->Count);
		//sb->Append("\r\n");
		//sb->Append("SortElementsUp = ");
		//sb->Append(this->SortElementsTop->Count);
		//sb->Append("\r\n");
		//sb->Append("SortElementsDown = ");
		//sb->Append(this->SortElementsBottom->Count);
		//sb->Append("\r\n");
		sb->Append("\r\n");
		sb->Append("lastrightclick = ");
		sb->Append(this->lastrightclick);
		sb->Append("\r\n");
		sb->Append("\r\n");
		sb->Append("selectionelement = ");
		sb->Append(this->selectionelement->Count);
		sb->Append("\r\n");
		sb->Append("selectionpath = ");
		sb->Append(this->selectionpathjoint->Count);
		sb->Append("\r\n");
		sb->Append("selectionpathingjunction = ");
		sb->Append(this->selectionpathingjunction->Count);
		sb->Append("\r\n");
		sb->Append("selectionpathway = ");
		sb->Append(this->selectionpathway->Count);
		sb->Append("\r\n");
		sb->Append("selectionmode = ");
		sb->Append(this->selectionmode.ToString());
		sb->Append("\r\n");
		sb->Append("\r\n");
		sb->Append("isclick = ");
		sb->Append(this->isclick);
		sb->Append("\r\n");
		sb->Append("draganchor = ");
		sb->Append(this->draganchor);
		sb->Append("\r\n");
		sb->Append("isdragselecting = ");
		sb->Append(this->isdragselecting);
		sb->Append("\r\n");
		sb->Append("selectionboxpoint = ");
		sb->Append(this->selectionboxpoint);
		sb->Append("\r\n");

		g->DrawString(sb->ToString(), this->courierfont, Brushes::Blue, 5, 5);
	}

	for each (Element^ e in this->Elements)
	{
		if (this->SelectionMode == SelectionModeType::Element && this->selectionelement->Contains(e))
		{
			e->Render(g, this->selectionpen, this->courierfont);
		}
		else
		{
			e->Render(g, Pens::Black, this->courierfont);
		}
	}

	Set<Path^>^	nojointpaths;
	nojointpaths = gcnew Set<Path^>;
	for each (PathJoint^ pj in this->selectionpathjoint)
	{
		nojointpaths->Add(pj->ParentPath);
	}
	for each (Path^ p in this->Paths)
	{
		if (nojointpaths->Contains(p))
		{
			p->Render(g, Pens::Black, false);

			int			i;
			int			j;
			PathJoint^	pj;

			j = p->PathJoints->Count;
			if (p->To)
			{
				j--;
			}
			for (i = 0; i < j; i++)
			{
				pj = p->PathJoints[i];
				if (this->selectionpathjoint->Contains(pj))
				{
					pj->Render(g, this->selectionpen);
				}
				else
				{
					pj->Render(g, Pens::Black);
				}
			}
			if (p->To)
			{
				pj = p->PathJoints[j];
				if (this->selectionpathjoint->Contains(pj))
				{
					pj->RenderArrow(g, this->selectionpen, p->PathJoints[j - 1]->Location);
				}
				else
				{
					pj->RenderArrow(g, Pens::Black, p->PathJoints[j - 1]->Location);
				}
			}
		}
		else
		{
			p->Render(g, Pens::Black, true);
		}
	}

	for each (PathingJunction^ p in this->PathingJunctions)
	{
		if (this->SelectionMode == SelectionModeType::PathingJunction && this->selectionpathingjunction->Contains(p))
		{
			p->Render(g, this->selectionpen);
		}
		else
		{
			p->Render(g, Pens::Orange);
		}
	}

	for each (Pathway^ p in this->Pathways)
	{
		if (this->SelectionMode == SelectionModeType::Pathway && this->selectionpathway->Contains(p))
		{
			p->Render(g, this->selectionpen);
		}
		else
		{
			p->Render(g, Pens::Orange);
		}
	}

	if (this->isdragselecting && this->isclick)
	{
		Rectangle		r;
		Point			pt;
		Drawing::Size	s;

		pt = this->selectionboxpoint;
		s.Width = Math::Abs(pt.X - this->draganchor.X);
		s.Height = Math::Abs(pt.Y - this->draganchor.Y);
		pt.X = Math::Min(this->draganchor.X, pt.X);
		pt.Y = Math::Min(this->draganchor.Y, pt.Y);
		r.Location = pt;
		r.Size = s;
		g->DrawRectangle(Pens::Gray, r);
	}
}

ElementCollection^ CrystallineControl::Elements::get(void)
{
	return this->elements;
}

Set<Path^>^ CrystallineControl::Paths::get(void)
{
	return this->paths;
}

void CrystallineControl::CrystallineControl_MouseDown(Object^, MouseEventArgs^ e)
{
	//this->selectionelement->Clear();
	//this->selectionpathjoint->Clear();

	//get element at cursor
	//if none, start drag-selection-box

	if (e->Button == Forms::MouseButtons::Left)
	{
		this->isclick = true;
		this->draganchor = e->Location;

		if (this->SelectionMode == SelectionModeType::Element)
		{
			Set<Element^>^ s1;
			Set<Element^>^ s2;

			s1 = this->GetElementsAtPoint(e->Location);
			if (s1->Count > 0)
			{
				s2 = this->selectionelement->Intersection(s1);
				if (s2->Count > 0)
				{
					//clicked a previously-selected element
				}
				else
				{
					//clicked a different element
					this->selectionelement->Clear();
					for each (Element^ ee in s1)
					{
						this->selectionelement->Add(ee);
					}
				}
				this->isdragselecting = false;
			}
			else
			{
				this->selectionelement->Clear();
				this->isdragselecting = true;
			}
		}
		else if (this->SelectionMode == SelectionModeType::Path)
		{
			Set<PathJoint^>^	s1;
			Set<PathJoint^>^	s2;

			s1 = this->GetPathJointsAtPoint(e->Location);
			if (s1->Count > 0)
			{
				s2 = this->selectionpathjoint->Intersection(s1);
				if (s2->Count > 0)
				{
					//clicked a previously-selected element
				}
				else
				{
					//clicked a different element
					this->selectionpathjoint->Clear();
					for each (PathJoint^ ee in s1)
					{
						this->selectionpathjoint->Add(ee);
					}
				}
				this->isdragselecting = false;
			}
			else
			{
				this->selectionpathjoint->Clear();
				this->isdragselecting = true;
			}
		}
		else if (this->SelectionMode == SelectionModeType::PathingJunction)
		{
			Set<PathingJunction^>^ s1;
			Set<PathingJunction^>^ s2;

			s1 = this->GetPathingJunctionsAtPoint(e->Location);
			if (s1->Count > 0)
			{
				s2 = this->selectionpathingjunction->Intersection(s1);
				if (s2->Count > 0)
				{
					//clicked a previously-selected element
				}
				else
				{
					//clicked a different element
					this->selectionpathingjunction->Clear();
					for each (PathingJunction^ ee in s1)
					{
						this->selectionpathingjunction->Add(ee);
					}
				}
				this->isdragselecting = false;
			}
			else
			{
				this->selectionpathingjunction->Clear();
				this->isdragselecting = true;
			}
		}
		else if (this->SelectionMode == SelectionModeType::Pathway)
		{
			Set<Pathway^>^ s1;
			Set<Pathway^>^ s2;

			s1 = this->GetPathwaysAtPoint(e->Location);
			if (s1->Count > 0)
			{
				s2 = this->selectionpathway->Intersection(s1);
				if (s2->Count > 0)
				{
					//clicked a previously-selected element
				}
				else
				{
					//clicked a different element
					this->selectionpathway->Clear();
					for each (Pathway^ ee in s1)
					{
						this->selectionpathway->Add(ee);
					}
				}
				this->isdragselecting = false;
			}
			else
			{
				this->selectionpathway->Clear();
				this->isdragselecting = true;
			}
		}

	}

}

void CrystallineControl::CrystallineControl_MouseMove(Object^, MouseEventArgs^ e)
{
	//this->Invalidate();

	if (e->Button == Forms::MouseButtons::Middle)
	{
		e = e;
	}

	if (e->Button == Forms::MouseButtons::Left)
	{
		if (this->isclick)
		{
			if (this->isdragselecting)
			{
				//draw the box (do nothing here)
				this->selectionboxpoint = e->Location;
			}
			else
			{
				Drawing::Size	delta;
				delta = Drawing::Size(Point::Subtract(e->Location, Drawing::Size(this->draganchor)));

				//move the selection
				if (this->SelectionMode == SelectionModeType::Element)
				{
					Set<Path^>^	wholepaths;
					wholepaths = gcnew Set<Path^>;

					for each (Element^ ee in this->selectionelement)
					{
						for each (Path^ p in ee->Inbound)
						{
							if (!wholepaths->Contains(p) &&
								p->To && this->selectionelement->Contains(p->To) && 
								p->From && this->selectionelement->Contains(p->From))
							{
								wholepaths->Add(p);
							}
						}
					}

					for each (Element^ ve in this->selectionelement)
					{
						//ve->Location += delta;
						ve->Move(ve->Location + delta);
						for each (Path^ p in ve->Inbound)
						{
							if (!wholepaths->Contains(p))
							{
								//p->PathJoints[p->PathJoints->Count - 1]->Location += delta;
								this->RoutePath(p);
							}
						}
						for each (Path^ p in ve->Outbound)
						{
							if (!wholepaths->Contains(p))
							{
								//p->PathJoints[0]->Location += delta;
								this->RoutePath(p);
							}
						}
					}

					int	i;
					int	j;
					for each (Path^ p in wholepaths)
					{
						j = p->PathJoints->Count;
						for (i = 0; i < j; i++)
						{
							p->PathJoints[i]->Location += delta;
						}
					}
				}
				else if (this->SelectionMode == SelectionModeType::Path)	//this->selectingelement
				{
					Path^	p;
					PointF	pt;
					bool	first;

					if (this->selectionpathjoint->Count == 1)
					{
						for each (PathJoint^ pj in this->selectionpathjoint)
						{
							if (pj == pj->ParentPath->PathJoints->First || pj == pj->ParentPath->PathJoints->Last)
							{
								if (pj == pj->ParentPath->PathJoints->First)
								{
									first = true;
								}
								p = pj->ParentPath;
								pt = pj->Location;
							}
						}
					}

					if (p)
					{
						Set<Element^>^	s;
						s = this->GetElementsAtPoint(pt);
						if (s->Count > 0)
						{
							for each (Element^ e in s)
							{
								if (first)
								{
									p->From = e;
								}
								else
								{
									p->To = e;
								}

								break;
							}
						}
						else
						{
							if (first)
							{
								p->From = nullptr;
							}
							else
							{
								p->To = nullptr;
							}
						}
					}

					for each (PathJoint^ ve in this->selectionpathjoint)
					{
						ve->Location += delta;
						if (!p)
						{
							if (ve == ve->ParentPath->PathJoints->First)
							{
								ve->ParentPath->From = nullptr;
							}
							else if (ve == ve->ParentPath->PathJoints->Last)
							{
								ve->ParentPath->To = nullptr;
							}
						}
					}
				}
				else if (this->SelectionMode == SelectionModeType::PathingJunction)
				{
					for each (PathingJunction^ p in this->selectionpathingjunction)
					{
						//p->X += delta.Width;
						//p->Y += delta.Height;
						p->Move(p->Location + delta);
					}
				}
				else if (this->SelectionMode == SelectionModeType::Pathway)
				{
					for each (Pathway^ p in this->selectionpathway)
					{
						p->Move(p->Location + delta);
					}
				}

				this->draganchor = e->Location;
			}
			this->Refresh();
		}
	}
}

void CrystallineControl::CrystallineControl_MouseUp(Object^, MouseEventArgs^ e)
{
	MouseEventArgs^	_e = e;

	if (e->Button == Forms::MouseButtons::Left)
	{
		if (this->isdragselecting)
		{
			if (this->Dist(this->draganchor, e->Location) < 3)
			{
				if (this->SelectionMode == SelectionModeType::Element)
				{
					//this->ElementSelection->Clear();
					//this->ElementSelection->Add
					//this->ElementSelection = this->flowchart->GetElementsAtPoint(this->draganchor);
					Set<Element^>^	s1;
					s1 = this->GetElementsAtPoint(this->draganchor);
					this->selectionelement->Clear();
					for each (Element^ ee in s1)
					{
						this->selectionelement->Add(ee);
					}
				}
				else if (this->SelectionMode == SelectionModeType::Path)
				{
					Set<PathJoint^>^	s1;
					s1 = this->GetPathJointsAtPoint(this->draganchor);
					this->selectionpathjoint->Clear();
					for each (PathJoint^ ee in s1)
					{
						this->selectionpathjoint->Add(ee);
					}
				}
				else if (this->SelectionMode == SelectionModeType::PathingJunction)
				{
					Set<PathingJunction^>^	s1;
					s1 = this->GetPathingJunctionsAtPoint(this->draganchor);
					this->selectionpathingjunction->Clear();
					for each (PathingJunction^ p in s1)
					{
						this->selectionpathingjunction->Add(p);
					}
				}
				else if (this->SelectionMode == SelectionModeType::Pathway)
				{
					Set<Pathway^>^	s1;
					s1 = this->GetPathwaysAtPoint(this->draganchor);
					this->selectionpathway->Clear();
					for each (Pathway^ p in s1)
					{
						this->selectionpathway->Add(p);
					}
				}
			}
			else
			{
				Rectangle		r;
				Point			pt;
				Drawing::Size	s;

				pt = e->Location;//this->MainPanel->PointToClient(this->MainPanel->MousePosition);
				s.Width = Math::Abs(pt.X - this->draganchor.X);
				s.Height = Math::Abs(pt.Y - this->draganchor.Y);
				pt.X = Math::Min(this->draganchor.X, pt.X);
				pt.Y = Math::Min(this->draganchor.Y, pt.Y);
				r.Location = pt;
				r.Size = s;

				if (this->SelectionMode == SelectionModeType::Element)
				{
					Set<Element^>^	set;
					set = this->GetElementsInRect(r);
					this->selectionelement->Clear();
					for each (Element^ ee in set)
					{
						this->selectionelement->Add(ee);
					}
				}
				else if (this->SelectionMode == SelectionModeType::Path)
				{
					Set<PathJoint^>^	set;
					set = this->GetPathJointsInRect(r);
					this->selectionpathjoint->Clear();
					for each (PathJoint^ ee in set)
					{
						this->selectionpathjoint->Add(ee);
					}
				}
				else if (this->SelectionMode == SelectionModeType::PathingJunction)
				{
					Set<PathingJunction^>^	set;
					set = this->GetPathingJunctionsInRect(r);
					this->selectionpathingjunction->Clear();
					for each (PathingJunction^ p in set)
					{
						this->selectionpathingjunction->Add(p);
					}
				}
				else if (this->SelectionMode == SelectionModeType::Pathway)
				{
					Set<Pathway^>^	set;
					set = this->GetPathwaysInRect(r);
					this->selectionpathway->Clear();
					for each (Pathway^ p in set)
					{
						this->selectionpathway->Add(p);
					}
				}
			}
		}
		else
		{
			if (this->draganchor == e->Location)
			{
			}
			else
			{
				//this->SortAllElementLists();
			}
		}

		this->isclick = false;
		this->isdragselecting = false;
		this->Refresh();
	}

	if (e->Button == Forms::MouseButtons::Right)
	{
		this->isclick = false;
		this->isdragselecting = false;

		this->lastrightclick = e->Location;
		this->UpdateContextMenu();
		this->ContextMenuStrip->Show(this, e->Location);
	}
}

void CrystallineControl::SetupContextMenu(void)
{
	this->ContextMenuStrip = gcnew Forms::ContextMenuStrip;

	ToolStripMenuItem^	item;
	ToolStripMenuItem^	item2;

	item = this->CreateElementItem = gcnew ToolStripMenuItem;
	item->Text = "Create Element";
	item->Name = "CreateElementItem";
	item->Click += gcnew EventHandler(this, &CrystallineControl::CreateElementItem_Click);
	this->ContextMenuStrip->Items->Add(item);

	item = this->CreatePathItem = gcnew ToolStripMenuItem;
	item->Text = "Create Path";
	item->Name = "CreatePathItem";
	item->Click += gcnew EventHandler(this, &CrystallineControl::CreatePathItem_Click);
	this->ContextMenuStrip->Items->Add(item);

	item = this->CreatePathingJunctionItem = gcnew ToolStripMenuItem;
	item->Text = "Create PathingJunction";
	item->Name = "CreatePathingJunctionItem";
	item->Click += gcnew EventHandler(this, &CrystallineControl::CreatePathingJunctionItem_Click);
	this->ContextMenuStrip->Items->Add(item);

	item = this->CreatePathwayItem = gcnew ToolStripMenuItem;
	item->Text = "Create Pathway";
	item->Name = "CreatePathwayItem";
	item->Click += gcnew EventHandler(this, &CrystallineControl::CreatePathwayItem_Click);
	this->ContextMenuStrip->Items->Add(item);

	this->ContextMenuStrip->Items->Add(gcnew ToolStripSeparator);

	item = this->DeleteItem = gcnew ToolStripMenuItem;
	item->Text = "Delete";
	item->Name = "DeleteItem";
	item->Click += gcnew EventHandler(this, &CrystallineControl::DeleteItem_Click);
	this->ContextMenuStrip->Items->Add(item);

	this->ContextMenuStrip->Items->Add(gcnew ToolStripSeparator);

	item = this->SelectElementItem = gcnew ToolStripMenuItem;
	item->Text = "Select Element";
	item->Name = "SelectElementItem";
	item->Click += gcnew EventHandler(this, &CrystallineControl::SelectElementItem_Click);
	this->ContextMenuStrip->Items->Add(item);

	item = this->SelectPathItem = gcnew ToolStripMenuItem;
	item->Text = "Select Path";
	item->Name = "SelectPathItem";
	item->Click += gcnew EventHandler(this, &CrystallineControl::SelectPathItem_Click);
	this->ContextMenuStrip->Items->Add(item);

	item = this->SelectPathingJunctionItem = gcnew ToolStripMenuItem;
	item->Text = "Select PathingJunction";
	item->Name = "SelectPathingJunctionItem";
	item->Click += gcnew EventHandler(this, &CrystallineControl::SelectPathingJunctionItem_Click);
	this->ContextMenuStrip->Items->Add(item);

	item = this->SelectPathwayItem = gcnew ToolStripMenuItem;
	item->Text = "Select Pathway";
	item->Name = "SelectPathwayItem";
	item->Click += gcnew EventHandler(this, &CrystallineControl::SelectPathwayItem_Click);
	this->ContextMenuStrip->Items->Add(item);

	this->ContextMenuStrip->Items->Add(gcnew ToolStripSeparator);

	item = this->CleanupItem = gcnew ToolStripMenuItem;
	item->Text = "Cleanup";
	item->Name = "CleanupItem";
	item->Click += gcnew EventHandler(this, &CrystallineControl::CleanupItem_Click);
	this->ContextMenuStrip->Items->Add(item);

	this->ContextMenuStrip->Items->Add(gcnew ToolStripSeparator);

	item2 = this->AddPathwayItem = gcnew ToolStripMenuItem();
	item2->Text = "Add Pathway";
	item2->Name = "AddPathwayItem";
	this->ContextMenuStrip->Items->Add(item2);

	item = this->AddPathwayLeftItem = gcnew ToolStripMenuItem();
	item->Text = "Left";
	item->Name = "AddPathwayLeftItem";
	item->Click += gcnew EventHandler(this, &CrystallineControl::AddPathwayLeftItem_Click);
	item2->DropDownItems->Add(item);

	item = this->AddPathwayRightItem = gcnew ToolStripMenuItem();
	item->Text = "Right";
	item->Name = "AddPathwayRightItem";
	item->Click += gcnew EventHandler(this, &CrystallineControl::AddPathwayRightItem_Click);
	item2->DropDownItems->Add(item);

	item = this->AddPathwayUpItem = gcnew ToolStripMenuItem();
	item->Text = "Up";
	item->Name = "AddPathwayUpItem";
	item->Click += gcnew EventHandler(this, &CrystallineControl::AddPathwayUpItem_Click);
	item2->DropDownItems->Add(item);

	item = this->AddPathwayDownItem = gcnew ToolStripMenuItem();
	item->Text = "Down";
	item->Name = "AddPathwayDownItem";
	item->Click += gcnew EventHandler(this, &CrystallineControl::AddPathwayDownItem_Click);
	item2->DropDownItems->Add(item);

}

void CrystallineControl::UpdateContextMenu(void)
{
	if (this->SelectionMode == SelectionModeType::Element)
	{
		if (this->selectionelement->Count > 0)
		{
			this->DeleteItem->Enabled = true;
		}
		else
		{
			this->DeleteItem->Enabled = false;
		}

		this->SelectElementItem->Checked = true;
		this->SelectPathItem->Checked = false;
		this->SelectPathingJunctionItem->Checked = false;
		this->SelectPathwayItem->Checked = false;

		this->AddPathwayItem->Enabled = false;
	}
	else if (this->SelectionMode == SelectionModeType::Path)
	{
		if (this->selectionpathjoint->Count > 0)
		{
			this->DeleteItem->Enabled = true;
		}
		else
		{
			this->DeleteItem->Enabled = false;
		}

		this->SelectElementItem->Checked = false;
		this->SelectPathItem->Checked = true;
		this->SelectPathingJunctionItem->Checked = false;
		this->SelectPathwayItem->Checked = false;

		this->AddPathwayItem->Enabled = false;
	}
	else if (this->SelectionMode == SelectionModeType::PathingJunction)
	{
		if (this->selectionpathingjunction->Count > 0)
		{
			this->DeleteItem->Enabled = true;
		}
		else
		{
			this->DeleteItem->Enabled = false;
		}

		this->SelectElementItem->Checked = false;
		this->SelectPathItem->Checked = false;
		this->SelectPathingJunctionItem->Checked = true;
		this->SelectPathwayItem->Checked = false;

		if (this->selectionpathingjunction->Count > 0)
		{
			this->AddPathwayItem->Enabled = true;
		}
		else
		{
			this->AddPathwayItem->Enabled = false;
		}
	}
	else if (this->SelectionMode == SelectionModeType::Pathway)
	{
		if (this->selectionpathway->Count > 0)
		{
			this->DeleteItem->Enabled = true;
		}
		else
		{
			this->DeleteItem->Enabled = false;
		}

		this->SelectElementItem->Checked = false;
		this->SelectPathItem->Checked = false;
		this->SelectPathingJunctionItem->Checked = false;
		this->SelectPathwayItem->Checked = true;

		this->AddPathwayItem->Enabled = false;
	}
}

void CrystallineControl::CreateElementItem_Click(Object^, EventArgs^)
{
	Element^	e;
	e = this->CreateElement();
	e->Location = this->lastrightclick;
	e->Size = SizeF(50, 20);
	e->Param = this->names[this->Elements->Count % this->names->Count];
	this->Elements->Add(e);
	this->Invalidate();
}

void CrystallineControl::CreatePathItem_Click(Object^, EventArgs^)
{
	Path^		p;
	PathJoint^	pj;

	p = this->CreatePath();

	pj = gcnew PathJoint(this->lastrightclick);
	p->PathJoints->Add(pj);

	pj = gcnew PathJoint(this->lastrightclick + Drawing::Size(10, 0));
	p->PathJoints->Add(pj);

	this->Paths->Add(p);
	this->Invalidate();
}

void CrystallineControl::DeleteItem_Click(Object^, EventArgs^)
{
	if (this->SelectionMode == SelectionModeType::Element)
	{
		Set<Element^>^	s;
		s = gcnew Set<Element^>;
		for each (Element^ e in this->selectionelement)
		{
			s->Add(e);
		}
		Set<Path^>^	removepaths;
		removepaths = gcnew Set<Path^>;
		for each (Element^ e in s)
		{
			e->Inbound->Clear();
			e->Outbound->Clear();
			this->Elements->Remove(e);
			this->selectionelement->Remove(e);
			delete e;
		}
		this->selectionelement->Clear();
	}
	else if (this->SelectionMode == SelectionModeType::Path)
	{
		Set<Path^>^	s;
		s = gcnew Set<Path^>;
		for each (PathJoint^ e in this->selectionpathjoint)
		{
			s->Add(e->ParentPath);
			if (e->ParentPath->From && e == e->ParentPath->PathJoints[0])
			{
				e->ParentPath->From = nullptr;
			}
			if (e->ParentPath->To && e == e->ParentPath->PathJoints[e->ParentPath->PathJoints->Count - 1])
			{
				e->ParentPath->To = nullptr;
			}
			e->ParentPath = nullptr;
		}
		//for each (Path^ e in s)
		//{
		//	//this->Paths->Remove(e);
		//	if (
		//}
		this->selectionpathjoint->Clear();
	}
	else if (this->SelectionMode == SelectionModeType::PathingJunction)
	{
		Set<PathingJunction^>^	s;
		s = gcnew Set<PathingJunction^>;
		for each (PathingJunction^ p in this->selectionpathingjunction)
		{
			s->Add(p);
		}
		Set<Path^>^	removepaths;
		removepaths = gcnew Set<Path^>;
		for each (PathingJunction^ p in s)
		{
			p->UpPathway = nullptr;
			p->DownPathway = nullptr;
			p->LeftPathway = nullptr;
			p->RightPathway = nullptr;
			p->EUp = nullptr;
			p->EDown = nullptr;
			p->ELeft = nullptr;
			p->ERight = nullptr;

			this->PathingJunctions->Remove(p);
		}
		this->selectionpathingjunction->Clear();
	}
	else if (this->SelectionMode == SelectionModeType::Pathway)
	{
		Debug::Fail(__WCODESIG__);
	}

	this->Invalidate();
}

Element^ CrystallineControl::CreateElement(void)
{
	Element^	e = gcnew Element();
	//e->Framework = this->Framework;
	return e;
}

Path^ CrystallineControl::CreatePath(void)
{
	return gcnew Path;
}

Set<Element^>^ CrystallineControl::GetElementsAtPoint(PointF pf)
{
	Set<Element^>^	set;

	set = gcnew Set<Element^>;

	for each (Element^ e in this->Elements)
	{
		if (e->Rect.Contains(pf))
		{
			set->Add(e);
		}
	}

	return set;
}

Set<PathJoint^>^ CrystallineControl::GetPathJointsAtPoint(PointF pf)
{
	Set<PathJoint^>^	set;
	set = gcnew Set<PathJoint^>;

	RectangleF	r;
	float		size;

	size = 3;

	r.Location = pf;
	r.X -= size;
	r.Y -= size;
	r.Width = 2 * size + 1;
	r.Height = 2 * size + 1;

	//int	i;
	//int	j;
	//for each (Path^ p in this->Paths)
	//{
	//	j = p->Points->Count;
	//	for (i = 0; i < j; i++)
	//	{
	//		if (r.Contains(p->Points[i]))
	//		{
	//			set->Add(PathJoint(p, i));
	//		}
	//	}
	//}

	//return set;

	return this->GetPathJointsInRect(r);
}

double CrystallineControl::Dist(PointF p1, PointF p2)
{
	float	xx;
	float	yy;
	xx = (p1.X - p2.X);
	yy = (p1.Y - p2.Y);
	return Math::Sqrt(xx*xx+yy*yy);
}

Set<Element^>^ CrystallineControl::GetElementsInRect(RectangleF r)
{
	RectangleF _r = r;

	Set<Element^>^	out;

	out = gcnew Set<Element^>;//(gcnew ElementCompare);
	for each (Element^ ve in this->Elements)
	{
		if (ve->Rect.IntersectsWith(r))
		{
			out->Add(ve);
		}
	}

	return out;
}

Set<PathJoint^>^ CrystallineControl::GetPathJointsInRect(RectangleF r)
{
	Set<PathJoint^>^	set;
	set = gcnew Set<PathJoint^>;

	int	i;
	int	j;
	for each (Path^ p in this->Paths)
	{
		j = p->PathJoints->Count;
		for (i = 0; i < j; i++)
		{
			if (r.Contains(p->PathJoints[i]->Location))
			{
				set->Add(p->PathJoints[i]);
			}
		}
	}

	return set;
}

void CrystallineControl::SelectElementItem_Click(Object^, EventArgs^)
{
	this->SelectionMode = SelectionModeType::Element;
	this->Invalidate();
}

void CrystallineControl::SelectPathItem_Click(Object^, EventArgs^)
{
	this->SelectionMode = SelectionModeType::Path;
	this->Invalidate();
}

//void CrystallineControl::Cleanup(void)
//{
//	this->CleanupElements();
//	this->CleanupPaths();
//
//	this->Invalidate();
//
//	return ;
//
//	List<Element^>^	xorder;
//	List<Element^>^	yorder;
//
//	xorder = gcnew List<Element^>;
//	yorder = gcnew List<Element^>;
//
//	float	width;
//	float	height;
//
//	width = 50;
//	height = 20;
//
//	for each (Element^ e in this->Elements)
//	{
//		xorder->Add(e);
//		yorder->Add(e);
//
//		if (e->Size.Width > width) { width = e->Size.Width; }
//		if (e->Size.Height > height) { height = e->Size.Height; }
//	}
//
//	xorder->Sort(gcnew CrystallineControl::CleanupComparer(false));
//	yorder->Sort(gcnew CrystallineControl::CleanupComparer(true));
//
//	//int	rows;
//	//int columns;
//
//	//columns = (int)Math::Ceiling((xorder[xorder->Count - 1] - xorder[0]) / height);
//	//rows = (int)Math::Ceiling((yorder[yorder->Count - 1] - yorder[0]) / width);
//
//	width += 50;//this->Paths->Count * 10 + 10;
//	height += 50;//this->Paths->Count * 10 + 10;
//
//	for each (Element^ e in xorder)
//	{
//		e->Location = PointF(25, 25);
//	}
//	
//	int	i;
//	int	j;
//
//	j = xorder->Count;
//	for (i = 0; i < j; i++)
//	{
//		xorder[i]->Location += SizeF(i * width, 0);
//		yorder[i]->Location += SizeF(0, i * height);
//	}
//
//	List<List<Path^>^>^	rowchannels;
//	List<List<Path^>^>^	columnchannels;
//
//	rowchannels = gcnew List<List<Path^>^>;
//	columnchannels = gcnew List<List<Path^>^>;
//
//	this->selectionpathjoint->Clear();
//
//	Set<Path^>^	removepaths;
//	removepaths = gcnew Set<Path^>;
//	for each (Path^ p in this->Paths)
//	{
//		//p->PathJoints->Clear();
//
//		//float	x1;
//		//float	y1;
//		//float	x2;
//		//float	y2;
//		//float	x3;
//
//		//if (p->From)
//		//{
//		//	x1 = p->From->Rect.Right;
//		//	y1 = p->From->Location.Y + p->From->Size.Height / 2;
//		//}
//		//if (p->To)
//		//{
//		//	x2 = p->To->Location.X;
//		//	y2 = p->To->Location.Y + p->To->Size.Height / 2;
//		//}
//
//		//if (p->From && p->To)
//		//{
//		//	x3 = (x1 + x2) / 2;
//		//}
//
//		//if (p->From && p->To)
//		//{
//		//	p->PathJoints->Add(gcnew PathJoint(PointF(x1, y1)));
//		//	p->PathJoints->Add(gcnew PathJoint(PointF(x3, y1)));
//		//	p->PathJoints->Add(gcnew PathJoint(PointF(x3, y2)));
//		//	p->PathJoints->Add(gcnew PathJoint(PointF(x2, y2)));
//		//}
//		//else if (p->From)
//		//{
//		//	p->PathJoints->Add(gcnew PathJoint(PointF(x1, y1)));
//		//	p->PathJoints->Add(gcnew PathJoint(PointF(x1 + 10, y1)));
//		//}
//		//else if (p->To)
//		//{
//		//	p->PathJoints->Add(gcnew PathJoint(PointF(x2 - 10, y2)));
//		//	p->PathJoints->Add(gcnew PathJoint(PointF(x2, y2)));
//		//}
//		//else
//		if (p->From || p->To)
//		{
//			this->RoutePath(p);
//		}
//		else
//		{
//			removepaths->Add(p);
//		}
//	}
//	for each (Path^ p in removepaths)
//	{
//		this->Paths->Remove(p);
//	}
//
//	this->Invalidate();
//}

//void CrystallineControl::CleanupElements(void)
//{
//	////divide into cells
//	//
//	//List<Element^>^				elems;
//	//List<PointF>^				centers;
//	//array<Set<Element^>^, 2>^	cells;
//	//RectangleF					cellsize;
//	//RectangleF					gridsize;
//	//int							i;
//	//int							j;
//	//
//	//elems = gcnew List<Element^>(this->Elements->Count);
//	//for each (Element^ e in this->Elements)
//	//{
//	//	elems->Add(e);
//	//}
//	//
//	//centers = gcnew List<PointF>(elems->Count);
//	//
//	//cellsize = elems[0]->Rect;
//	//gridsize = elems[0]->Rect;
//	//j = elems->Count;
//	//for (i = 0; i < j; i++)
//	//{
//	//	if (elems[i]->Rect.Width > cellsize.Width)
//	//	{
//	//		cellsize.Width = elems[i]->Rect.Width;
//	//	}
//	//	if (elems[i]->Rect.Height > cellsize.Height)
//	//	{
//	//		cellsize.Height = elems[i]->Rect.Height;
//	//	}
//	//
//	//	gridsize = RectangleF::Union(gridsize, elems[i]->Rect);
//	//
//	//	centers->Add(PointF(elems[i]->Rect.Left + elems[i]->Rect.Width / 2,
//	//						elems[i]->Rect.Right + elems[i]->Rect.Height / 2));
//	//}
//	//
//	//i = (this->Paths->Count + 1) * 5;
//	//i += 15; // routing space next to element for incoming/outgoing paths
//	//cellsize.Width += i;
//	//cellsize.Height += i;
//	//
//	//cellsize.Width = (float)Math::Ceiling(gridsize.Width);
//	//cellsize.Height = (float)Math::Ceiling(gridsize.Height);
//	//gridsize.Width = (float)Math::Ceiling(gridsize.Width);
//	//gridsize.Height = (float)Math::Ceiling(gridsize.Height);
//	//
//	//if ((int)gridsize.Width % (int)cellsize.Width)
//	//{
//	//	gridsize.Width += cellsize.Width - ((int)gridsize.Width % (int)cellsize.Width);
//	//}
//	//if ((int)gridsize.Height % (int)cellsize.Height)
//	//{
//	//	gridsize.Height += cellsize.Height - ((int)gridsize.Height % (int)cellsize.Height);
//	//}
//	//
//	//int	cols;
//	//int	rows;
//	//
//	//cols = (int)gridsize.Width / (int)cellsize.Width;
//	//rows = (int)gridsize.Height / (int)cellsize.Height;
//	//
//	//while (cols * rows < elems->Count)
//	//{
//	//	if (cols > rows)
//	//	{
//	//		rows++;
//	//	}
//	//	else
//	//	{
//	//		cols++;
//	//	}
//	//}
//	//
//	//cells = gcnew array<Set<Element^>^, 2>(rows, cols);
//	//
//	//for (i = 0; i < rows; i++)
//	//{
//	//	for (j = 0; j < cols; j++)
//	//	{
//	//		cells[i, j] = gcnew Set<Element^>();
//	//	}
//	//}
//	//
//	//for (i = 0; i < elems->Count; i++)
//	//{
//	//	PointF	p;
//	//
//	//	p.X = elems[i]->Location.X - gridsize.X;
//	//	p.Y = elems[i]->Location.Y - gridsize.Y;
//	//
//	//	cells[(int)(p.X / cellsize.Width), (int)(p.Y / cellsize.Height)]->Add(elems[i]);
//	//}
//
//
//
//	//bump apart
//
//	List<Element^>^				elems;
//	SizeF						size;
//	int							i;
//	int							j;
//	
//	elems = gcnew List<Element^>(this->Elements->Count);
//	for each (Element^ e in this->Elements)
//	{
//		elems->Add(e);
//	}
//	
//	size = elems[0]->Size;
//
//	j = elems->Count;
//	for (i = 0; i < j; i++)
//	{
//		if (elems[i]->Size.Width > size.Width)
//		{
//			size.Width = elems[i]->Size.Width;
//		}
//		if (elems[i]->Size.Height > size.Height)
//		{
//			size.Height = elems[i]->Size.Height;
//		}
//	}
//	
//	//i = 6*5;//(this->Paths->Count + 1) * 5;	//spacing for paths will be governed by pathingjunctions
//	i += 15; // routing space next to element for incoming/outgoing paths
//	i += 15; // routing space next to other element for incoming/outgoing paths
//	size.Width += i;
//	size.Height += i;
//
//	bool	cont = true;
//
//	while (cont)
//	{
//		cont = false;
//		for each (Element^ e1 in elems)
//		{
//			RectangleF	r1;
//			r1 = e1->Rect;
//			r1.Size = size;
//			for each (Element^ e2 in elems)
//			{
//				if (e1 == e2) { continue; }
//
//				RectangleF	r2;
//				r2 = e2->Rect;
//				r2.Size = size;
//
//				if (r1.IntersectsWith(r2))
//				{
//					cont = true;
//					RectangleF	ri;
//					ri = RectangleF::Intersect(r1, r2);
//
//					PointF	p1;
//					PointF	p2;
//
//					p1 = e1->Location;
//					p2 = e2->Location;
//
//					float	f;
//
//					if (ri.Width > ri.Height)
//					{
//						//separate up/down
//						f = (float)Math::Ceiling(ri.Height/2)+1;
//						if (p1.Y > p2.Y)
//						{
//							p1.Y += f;
//							p2.Y -= f;
//						}
//						else
//						{
//							p2.Y += f;
//							p1.Y -= f;
//						}
//					}
//					else
//					{
//						//separate X/right
//						f = (float)Math::Ceiling(ri.Width/2)+1;
//						if (p1.X > p2.X)
//						{
//							p1.X += f;
//							p2.X -= f;
//						}
//						else
//						{
//							p2.X += f;
//							p2.X -= f;
//						}
//					}
//
//					e1->Location = p1;
//					e2->Location = p2;
//
//					//this->Invalidate();
//					//this->Update();
//					//Threading::Thread::Sleep(1000);
//				}
//			}
//		}
//	}
//}

//void CrystallineControl::CleanupPaths(void)
//{
//	this->selectionpathjoint->Clear();
//
//	Set<Path^>^	removepaths;
//	removepaths = gcnew Set<Path^>;
//	for each (Path^ p in this->Paths)
//	{
//		if (p->From || p->To)
//		{
//			this->RoutePath(p);
//		}
//		else
//		{
//			removepaths->Add(p);
//		}
//	}
//	for each (Path^ p in removepaths)
//	{
//		this->Paths->Remove(p);
//	}
//}

//void CrystallineControl::Cleanup3(void)
//{
//	this->CleanupElements();
//	this->CleanupPaths();
//
//	this->Invalidate();
//}

void CrystallineControl::CleanupItem_Click(Object^, EventArgs^)
{
	//this->Cleanup();
}

void CrystallineControl::RoutePath(Path^ p)
{
	Path^ _p = p;

	if (!p->To && !p->From)
	{
		float	x;
		float	y;

		x = 0;
		y = 0;
		for each (PathJoint^ pj in p->PathJoints)
		{
			x += pj->Location.X;
			y += pj->Location.Y;
		}

		if (p->PathJoints->Count > 0)
		{
			x /= p->PathJoints->Count;
			y /= p->PathJoints->Count;
		}

		p->PathJoints->Clear();
		p->PathJoints->Add(gcnew PathJoint(PointF(x, y)));
		p->PathJoints->Add(gcnew PathJoint(PointF(x + 20, y)));
		return ;
	}

	p->PathJoints->Clear();

	float	x1;
	float	y1;
	float	x2;
	float	y2;
	float	x3;

	if (p->From)
	{
		x1 = p->From->Rect.Right;
		y1 = p->From->Location.Y + p->From->Size.Height / 2;
	}
	if (p->To)
	{
		x2 = p->To->Location.X;
		y2 = p->To->Location.Y + p->To->Size.Height / 2;
	}

	if (p->From && p->To)
	{
		x3 = (x1 + x2) / 2;
	}

	if (p->From && p->To)
	{
		p->PathJoints->Add(gcnew PathJoint(PointF(x1, y1)));
		p->PathJoints->Add(gcnew PathJoint(PointF(x3, y1)));
		p->PathJoints->Add(gcnew PathJoint(PointF(x3, y2)));
		p->PathJoints->Add(gcnew PathJoint(PointF(x2, y2)));
	}
	else if (p->From)
	{
		p->PathJoints->Add(gcnew PathJoint(PointF(x1, y1)));
		p->PathJoints->Add(gcnew PathJoint(PointF(x1 + 20, y1)));
	}
	else if (p->To)
	{
		p->PathJoints->Add(gcnew PathJoint(PointF(x2 - 20, y2)));
		p->PathJoints->Add(gcnew PathJoint(PointF(x2, y2)));
	}
}

void CrystallineControl::ConnectInboundPath(Path^, Element^, PointF)
{
	
}

void CrystallineControl::ConnectOutboundPath(Path^, Element^, PointF)
{
	
}

PathingJunctionCollection^ CrystallineControl::PathingJunctions::get(void)
{
	return this->pathingjunctions;
}

PathwayCollection^ CrystallineControl::Pathways::get(void)
{
	return this->pathways;
}

void CrystallineControl::CreatePathingJunctionItem_Click(Object^, EventArgs^)
{
	PathingJunction^	p;
	p = this->CreatePathingJunction();

	//p->Location = this->lastrightclick;
	p->X = (float)this->lastrightclick.X;
	p->Y = (float)this->lastrightclick.Y;

	//p->Size = SizeF(50, 20);

	this->PathingJunctions->Add(p);
	this->Invalidate();
}

void CrystallineControl::SelectPathingJunctionItem_Click(Object^, EventArgs^)
{
	this->SelectionMode = SelectionModeType::PathingJunction;
	this->Invalidate();
}

void CrystallineControl::CreatePathwayItem_Click(Object^, EventArgs^)
{
	Pathway^	p;
	p = this->CreatePathway();

	//p->Location = this->lastrightclick;
	//p->X = (float)this->lastrightclick.X;
	//p->Y = (float)this->lastrightclick.Y;

	//p->Size = SizeF(50, 20);

	this->Pathways->Add(p);
	this->Invalidate();
}

void CrystallineControl::SelectPathwayItem_Click(Object^, EventArgs^)
{
	this->SelectionMode = SelectionModeType::Pathway;
	this->Invalidate();
}

Set<PathingJunction^>^ CrystallineControl::GetPathingJunctionsAtPoint(PointF pf)
{
	Set<PathingJunction^>^	set;

	set = gcnew Set<PathingJunction^>;

	for each (PathingJunction^ p in this->PathingJunctions)
	{
		if (p->Rect.Contains(pf))
		{
			set->Add(p);
		}
	}

	return set;
}

Set<PathingJunction^>^ CrystallineControl::GetPathingJunctionsInRect(RectangleF r)
{
	RectangleF _r = r;

	Set<PathingJunction^>^	out;

	out = gcnew Set<PathingJunction^>;//(gcnew ElementCompare);
	for each (PathingJunction^ p in this->PathingJunctions)
	{
		if (p->Rect.IntersectsWith(r))
		{
			out->Add(p);
		}
	}

	return out;
}

Set<Pathway^>^ CrystallineControl::GetPathwaysAtPoint(PointF pf)
{
	PointF	_pf = pf;

	Set<Pathway^>^	set;

	set = gcnew Set<Pathway^>;

	for each (Pathway^ p in this->Pathways)
	{
		if (p->Rect.Contains(pf))
		{
			set->Add(p);
		}
	}

	return set;
}

Set<Pathway^>^ CrystallineControl::GetPathwaysInRect(RectangleF r)
{
	RectangleF _r = r;

	Set<Pathway^>^	out;

	out = gcnew Set<Pathway^>;//(gcnew ElementCompare);
	for each (Pathway^ p in this->Pathways)
	{
		if (p->Rect.IntersectsWith(r))
		{
			out->Add(p);
		}
	}

	return out;
}

PathingJunction^ CrystallineControl::CreatePathingJunction(void)
{
	return gcnew PathingJunction();
}

Pathway^ CrystallineControl::CreatePathway(void)
{
	return gcnew Pathway();
}

CrystallineControl::SelectionModeType CrystallineControl::SelectionMode::get(void)
{
	return this->selectionmode;
}

void CrystallineControl::SelectionMode::set(SelectionModeType smt)
{
	 if (this->selectionmode != smt)
	 {
		 this->selectionmode = smt;
	 }
}

//void CrystallineControl::SortElements(void)
//{
//	if (!this->SortElementsLeft)
//	{
//		this->SortElementsLeft = gcnew List<Element^>();
//	}
//	if (!this->SortElementsTop)
//	{
//		this->SortElementsTop = gcnew List<Element^>();
//	}
//	if (!this->SortElementsRight)
//	{
//		this->SortElementsRight = gcnew List<Element^>();
//	}
//	if (!this->SortElementsBottom)
//	{
//		this->SortElementsBottom = gcnew List<Element^>();
//	}
//
//	if (!this->SorterLeft)
//	{
//		this->SorterLeft = gcnew ElementComparer(true, false);
//	}
//	if (!this->SorterTop)
//	{
//		this->SorterTop = gcnew ElementComparer(true, true);
//	}
//	if (!this->SorterRight)
//	{
//		this->SorterRight = gcnew ElementComparer(false, false);
//	}
//	if (!this->SorterBottom)
//	{
//		this->SorterBottom = gcnew ElementComparer(false, true);
//	}
//
//	this->SortElementsLeft->Clear();
//	this->SortElementsTop->Clear();
//	this->SortElementsRight->Clear();
//	this->SortElementsBottom->Clear();
//
//	this->SortElementsLeft->AddRange(this->Elements);
//	this->SortElementsTop->AddRange(this->Elements);
//	this->SortElementsRight->AddRange(this->Elements);
//	this->SortElementsBottom->AddRange(this->Elements);
//
//	this->SortElementsLeft->Sort(this->SorterLeft);
//	this->SortElementsTop->Sort(this->SorterTop);
//	this->SortElementsRight->Sort(this->SorterRight);
//	this->SortElementsBottom->Sort(this->SorterBottom);
//
//}

void CrystallineControl::AddPathwayLeftItem_Click(Object^, EventArgs^)
{
	if (this->SelectionMode == SelectionModeType::PathingJunction)
	{
		for each (PathingJunction^ pj in this->selectionpathingjunction)
		{
			Pathway^	pw;

			pw = this->CreatePathway();
			pw->IsVertical = false;
			pw->X = pj->Left - pw->Width;
			pw->Y = pj->Y;
			pj->LeftPathway = pw;
			pw->RightDown = pj;
			this->Pathways->Add(pw);
		}

		this->Invalidate();
	}
}

void CrystallineControl::AddPathwayRightItem_Click(Object^, EventArgs^)
{
	if (this->SelectionMode == SelectionModeType::PathingJunction)
	{
		for each (PathingJunction^ pj in this->selectionpathingjunction)
		{
			Pathway^	pw;

			pw = this->CreatePathway();
			pw->IsVertical = false;
			pw->X = pj->Right;
			pw->Y = pj->Y;
			pj->RightPathway = pw;
			pw->LeftUp = pj;
			this->Pathways->Add(pw);
		}

		this->Invalidate();
	}
	
}

void CrystallineControl::AddPathwayUpItem_Click(Object^, EventArgs^)
{
	if (this->SelectionMode == SelectionModeType::PathingJunction)
	{
		for each (PathingJunction^ pj in this->selectionpathingjunction)
		{
			Pathway^	pw;

			pw = this->CreatePathway();
			pw->IsVertical = true;
			pw->X = pj->X;
			pw->Y = pj->Y - pw->Height;
			pj->UpPathway = pw;
			pw->RightDown = pj;
			this->Pathways->Add(pw);
		}

		this->Invalidate();
	}
}

void CrystallineControl::AddPathwayDownItem_Click(Object^, EventArgs^)
{
	if (this->SelectionMode == SelectionModeType::PathingJunction)
	{
		for each (PathingJunction^ pj in this->selectionpathingjunction)
		{
			Pathway^	pw;

			pw = this->CreatePathway();
			pw->IsVertical = true;
			pw->X = pj->X;
			pw->Y = pj->Bottom;
			pj->DownPathway = pw;
			pw->LeftUp = pj;
			this->Pathways->Add(pw);
		}

		this->Invalidate();
	}
}

//BoxFramework^ CrystallineControl::Framework::get(void)
//{
//	return this->framework;
//}



















































//CleanupComparer//

CrystallineControl::CleanupComparer::CleanupComparer(bool _sortbyy)
{
	this->sortbyy = _sortbyy;
}

int CrystallineControl::CleanupComparer::Compare(Element^ x, Element^ y)
{
	//if (this->sortbyy)
	//{
	//	if (x->Location.Y > y->Location.Y) return 1;
	//	if (x->Location.Y < y->Location.Y) return -1;
	//	if (x->Location.X > y->Location.X) return 1;
	//	if (x->Location.X < y->Location.X) return -1;
	//	return 0;
	//}
	//else
	//{
	//	if (x->Location.X > y->Location.X) return 1;
	//	if (x->Location.X < y->Location.X) return -1;
	//	if (x->Location.Y > y->Location.Y) return 1;
	//	if (x->Location.Y < y->Location.Y) return -1;
	//	return 0;
	//}

	if (this->sortbyy)
	{
		if (x->Location.Y > y->Location.Y) return 1;
		if (x->Location.Y < y->Location.Y) return -1;
	}
	if (x->Location.X > y->Location.X) return 1;
	if (x->Location.X < y->Location.X) return -1;
	if (!this->sortbyy)
	{
		if (x->Location.Y > y->Location.Y) return 1;
		if (x->Location.Y < y->Location.Y) return -1;
	}

	return 0;
}

//CleanupComparer//

//ElementComparer//

//CrystallineControl::ElementComparer::ElementComparer(bool _LeftTopVsRightBottom, bool _Vertical)
//{
//	this->lefttopvsrightbottom = _LeftTopVsRightBottom;
//	this->vertical = _Vertical;
//}
//
//int CrystallineControl::ElementComparer::Compare(Element^ x, Element^ y)
//{
//
//	if (this->lefttopvsrightbottom)
//	{
//		if (this->vertical)
//		{
//			//top
//			return x->Location.Y.CompareTo(y->Location.Y);
//		}
//		else
//		{
//			//left
//			return x->Location.X.CompareTo(y->Location.X);
//		}
//	}
//	else
//	{
//		if (this->vertical)
//		{
//			//bottom
//			return x->Rect.Bottom.CompareTo(y->Rect.Bottom);
//		}
//		else
//		{
//			//right
//			return x->Rect.Right.CompareTo(y->Rect.Right);
//		}
//	}
//
//	return 0;
//}


//ElementComparer//



NAMESPACE_END



