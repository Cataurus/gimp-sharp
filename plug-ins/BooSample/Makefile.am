## Process this file with automake to produce Makefile.in

all: BooSample.exe

GIMPSHARPLIB = $(top_builddir)/lib/gimp-sharp.dll

ASSEMBLIES = -r:$(GIMPSHARPLIB) -pkg:gtk-sharp-2.0 -target:winexe

MYSOURCES = BooSample.boo

EXTRA_DIST = $(MYSOURCES) BooSample BooSample.png

CLEANFILES = BooSample.exe

BooSample.exe : $(MYSOURCES) BooSample.png $(GIMPSHARPLIB)
	$(BOOC) $(MYSOURCES) -debug- -embedres:BooSample.png $(ASSEMBLIES) \
	-out:$@

install:
	$(INSTALL_DATA) BooSample.exe $(DESTDIR)$(GIMPPLUGINDIR)
	$(INSTALL) BooSample $(DESTDIR)$(GIMPPLUGINDIR)
