all: SliceTool.exe

ASSEMBLIES = -r $(top_builddir)/lib/gimp-sharp.dll /pkg:gtk-sharp

SOURCES =			\
	CreateFunc.cs		\
	CreateTableFunc.cs	\
	Format.cs		\
	HorizontalSlice.cs	\
	MouseFunc.cs		\
	Preview.cs		\
	PreviewRenderer.cs	\
	Rectangle.cs		\
	RectangleSet.cs		\
	RemoveFunc.cs		\
	SelectFunc.cs		\
	Slice.cs		\
	SliceData.cs		\
	SliceSet.cs		\
	SliceTool.cs		\
	TableDialog.cs		\
	VerticalSlize.cs

EXTRA_DIST = $(SOURCES) SliceTool SliceTool.png blank.png stock-arrow.png

SliceTool.exe : $(SOURCES) SliceTool.png
	$(CSC) $(SOURCES) /resource:SliceTool.png	\
			  /resource:blank.png		\
			  /resource:stock-arrow.png	\
		-o $@ $(ASSEMBLIES)

install:
	$(INSTALL_DATA) SliceTool.exe $(DESTDIR)/$(GIMPPLUGINDIR)
	$(INSTALL) SliceTool $(DESTDIR)/$(GIMPPLUGINDIR)