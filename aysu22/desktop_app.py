#!/usr/bin/env python3
import os
import sys
import gi

gi.require_version('Gtk', '4.0')
gi.require_version('WebKit', '6.0')
from gi.repository import Gtk, WebKit, Gio, GLib

class PulXirdalamaApp(Gtk.Application):
    def __init__(self):
        super().__init__(
            application_id='az.pul.xirdalama.desktop',
            flags=Gio.ApplicationFlags.FLAGS_NONE
        )

    def do_activate(self):
        win = Gtk.ApplicationWindow(application=self)
        win.set_title("Məbləğin Xırdalanması")
        win.set_default_size(1040, 680)

        # WebKit WebView
        webview = WebKit.WebView()
        settings = webview.get_settings()
        settings.set_enable_developer_extras(False)
        settings.set_javascript_can_open_windows_automatically(True)

        current_dir = os.path.dirname(os.path.abspath(__file__))
        html_file = os.path.join(current_dir, "index.html")
        webview.load_uri("file://" + html_file)

        win.set_child(webview)
        win.present()

if __name__ == "__main__":
    app = PulXirdalamaApp()
    sys.exit(app.run(sys.argv))
