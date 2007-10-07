/***************************************************************************
 *  LinkLabel.cs
 *
 *  Copyright (C) 2006 Novell, Inc.
 *  Written by Aaron Bockover <aaron@abock.org>
 ****************************************************************************/

/*  THIS FILE IS LICENSED UNDER THE MIT LICENSE AS OUTLINED IMMEDIATELY BELOW: 
 *
 *  Permission is hereby granted, free of charge, to any person obtaining a
 *  copy of this software and associated documentation files (the "Software"),  
 *  to deal in the Software without restriction, including without limitation  
 *  the rights to use, copy, modify, merge, publish, distribute, sublicense,  
 *  and/or sell copies of the Software, and to permit persons to whom the  
 *  Software is furnished to do so, subject to the following conditions:
 *
 *  The above copyright notice and this permission notice shall be included in 
 *  all copies or substantial portions of the Software.
 *
 *  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR 
 *  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
 *  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE 
 *  AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER 
 *  LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING 
 *  FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
 *  DEALINGS IN THE SOFTWARE.
 */
 
using System;
using Gtk;

namespace AstroGrep.Linux.Controls
{
    public class LinkLabel : EventBox
    {
        public delegate bool UriOpenHandler(string uri);
    
        private static UriOpenHandler default_open_handler;
        private static Gdk.Cursor hand_cursor = new Gdk.Cursor(Gdk.CursorType.Hand1);
        private static Gdk.Color link_color = new Gdk.Color(0, 0, 0xff);
        
        static LinkLabel()
        {
            Gdk.Colormap.System.AllocColor(ref link_color, true, true);
        }
        
        private Label label;
        private Uri uri;
        private bool is_pressed;
        private bool is_hovering;
        private UriOpenHandler open_handler;
        
        public event EventHandler Clicked; 
        
        public LinkLabel() : this(null, null)
        {
            Open = DefaultOpen;
        }
        
        public LinkLabel(string text, Uri uri)
        {
            CanFocus = true;
            AppPaintable = true;
            
            this.uri = uri;
            
            label = new Label(text);
            label.ModifyFg(Gtk.StateType.Normal, link_color);
            label.Show();
            Add(label);
        }
        
        protected virtual void OnClicked()
        {
            if(uri != null) {
                Open(uri.AbsoluteUri);
            }
        
            EventHandler handler = Clicked;
            if(handler != null) {
                handler(this, new EventArgs());
            }
        }
        
        protected override bool OnExposeEvent(Gdk.EventExpose evnt)
        {
            if(!IsDrawable) {
                return false;
            }
            
            if(evnt.Window == GdkWindow && HasFocus) {
                int layout_width = 0, layout_height = 0;
                label.Layout.GetPixelSize(out layout_width, out layout_height);
                
                int layout_x = evnt.Area.X + ((int)(evnt.Area.Width * label.Xalign) - 
                    (int)(layout_width * label.Xalign));
                int layout_y = evnt.Area.Y + ((int)(evnt.Area.Height * label.Yalign) - 
                    (int)(layout_height * label.Yalign));
               
                Style.PaintFocus(Style, GdkWindow, State, evnt.Area, 
                    this, "button", layout_x, layout_y, 
                    layout_width, layout_height);
            }
            
            if(Child != null) {
                PropagateExpose(Child, evnt);
            }
            
            return false;
        }
        
        protected override bool OnButtonPressEvent(Gdk.EventButton evnt)
        {
            if(evnt.Button == 1) {
                HasFocus = true;
                is_pressed = true;
            }
            
            return false;
        }
        
        protected override bool OnButtonReleaseEvent(Gdk.EventButton evnt)
        {
            if(evnt.Button == 1 && is_pressed && is_hovering) {
                OnClicked();
                is_pressed = false;
            }
            
            return false;
        }
        
        protected override bool OnKeyReleaseEvent(Gdk.EventKey evnt)
        {
            if(evnt.Key != Gdk.Key.KP_Enter && evnt.Key != Gdk.Key.Return 
                && evnt.Key != Gdk.Key.space) {
                return  false;
            }
            
            OnClicked();
            return false;
        }
        
        protected override bool OnEnterNotifyEvent(Gdk.EventCrossing evnt)
        {
            is_hovering = true;
            GdkWindow.Cursor = hand_cursor;
            return false;
        }

        protected override bool OnLeaveNotifyEvent(Gdk.EventCrossing evnt)
        {
            is_hovering = false;
            GdkWindow.Cursor = null;
            return false;
        }
        
        public string Text {
            get { return label.Text; }
            set { label.Text = value; }
        }
        
        public string Markup {
            set { label.Markup = value; }
        }
        
        public Label Label {
            get { return label; }
        }
        
        public float Xalign {
            get { return label.Xalign; }
            set { label.Xalign = value; }
        }
        
        public float Yalign {
            get { return label.Yalign; }
            set { label.Yalign = value; }
        }
        
        public Uri Uri {
            get { return uri; }
            set { uri = value; }
        }
        
        public string UriString {
            get { return uri == null ? null : uri.AbsoluteUri; }
            set { uri = new Uri(value); }
        }
        
        public UriOpenHandler Open {
            get { return open_handler; }
            set { open_handler = value; }
        }
        
        public static UriOpenHandler DefaultOpen {
            get {
                if(default_open_handler == null) {
                    //default_open_handler = new UriOpenHandler(Gnome.Url.Show);
                }
                
                return default_open_handler;
            }
            
            set { default_open_handler = value; }
        }
    }
}
