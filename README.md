# BahariModernUI
PKMKC

## PKM Karsa Cipta
RANCANG BANGUN APLIKASI TERANGI BAHARI DENGAN INTEL® REALSENSE™ TECHNOLOGY SEBAGAI SOLUSI EDUKASI KEKAYAAN BAHARI INDONESIA

## MIT License
Copyright (c) 2015, 2021 Terangi Bahari

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.

# How to Compile it
in BahariModernUI (project) --[right click]-> Properties -> Build Events
at Post-build event command line: insert ->
if "$(Platform)" == "x86" ( copy /y "$(RSSDK_DIR)\bin\win32\libpxccpp2c.dll" "$(TargetDir)" ) else ( copy /y "$(RSSDK_DIR)\bin\x64\libpxccpp2c.dll" "$(TargetDir)" )

# Compile
With new parameter inside code, now you can ignore above this section (How to Compile it)

# Dependencies
You must have been installed Intel RealSense SDK, plus .Net 4.5
