# CMAKE generated file: DO NOT EDIT!
# Generated by "Unix Makefiles" Generator, CMake Version 3.17

# Delete rule output on recipe failure.
.DELETE_ON_ERROR:


#=============================================================================
# Special targets provided by cmake.

# Disable implicit rules so canonical targets will work.
.SUFFIXES:


# Disable VCS-based implicit rules.
% : %,v


# Disable VCS-based implicit rules.
% : RCS/%


# Disable VCS-based implicit rules.
% : RCS/%,v


# Disable VCS-based implicit rules.
% : SCCS/s.%


# Disable VCS-based implicit rules.
% : s.%


.SUFFIXES: .hpux_make_needs_suffix_list


# Command-line flag to silence nested $(MAKE).
$(VERBOSE)MAKESILENT = -s

# Suppress display of executed commands.
$(VERBOSE).SILENT:


# A target that is always out of date.
cmake_force:

.PHONY : cmake_force

#=============================================================================
# Set environment variables for the build.

# The shell in which to execute make rules.
SHELL = /bin/sh

# The CMake executable.
CMAKE_COMMAND = /usr/local/Cellar/cmake/3.17.3/bin/cmake

# The command to remove a file.
RM = /usr/local/Cellar/cmake/3.17.3/bin/cmake -E rm -f

# Escaping for special characters.
EQUALS = =

# The top-level source directory on which CMake was run.
CMAKE_SOURCE_DIR = /Users/raghavprasad/Work/BITS/Thesis/fictrac

# The top-level build directory on which CMake was run.
CMAKE_BINARY_DIR = /Users/raghavprasad/Work/BITS/Thesis/fictrac/build

# Include any dependencies generated for this target.
include CMakeFiles/fictrac.dir/depend.make

# Include the progress variables for this target.
include CMakeFiles/fictrac.dir/progress.make

# Include the compile flags for this target's objects.
include CMakeFiles/fictrac.dir/flags.make

CMakeFiles/fictrac.dir/exec/fictrac.cpp.o: CMakeFiles/fictrac.dir/flags.make
CMakeFiles/fictrac.dir/exec/fictrac.cpp.o: ../exec/fictrac.cpp
	@$(CMAKE_COMMAND) -E cmake_echo_color --switch=$(COLOR) --green --progress-dir=/Users/raghavprasad/Work/BITS/Thesis/fictrac/build/CMakeFiles --progress-num=$(CMAKE_PROGRESS_1) "Building CXX object CMakeFiles/fictrac.dir/exec/fictrac.cpp.o"
	/Applications/Xcode.app/Contents/Developer/Toolchains/XcodeDefault.xctoolchain/usr/bin/c++  $(CXX_DEFINES) $(CXX_INCLUDES) $(CXX_FLAGS) -o CMakeFiles/fictrac.dir/exec/fictrac.cpp.o -c /Users/raghavprasad/Work/BITS/Thesis/fictrac/exec/fictrac.cpp

CMakeFiles/fictrac.dir/exec/fictrac.cpp.i: cmake_force
	@$(CMAKE_COMMAND) -E cmake_echo_color --switch=$(COLOR) --green "Preprocessing CXX source to CMakeFiles/fictrac.dir/exec/fictrac.cpp.i"
	/Applications/Xcode.app/Contents/Developer/Toolchains/XcodeDefault.xctoolchain/usr/bin/c++ $(CXX_DEFINES) $(CXX_INCLUDES) $(CXX_FLAGS) -E /Users/raghavprasad/Work/BITS/Thesis/fictrac/exec/fictrac.cpp > CMakeFiles/fictrac.dir/exec/fictrac.cpp.i

CMakeFiles/fictrac.dir/exec/fictrac.cpp.s: cmake_force
	@$(CMAKE_COMMAND) -E cmake_echo_color --switch=$(COLOR) --green "Compiling CXX source to assembly CMakeFiles/fictrac.dir/exec/fictrac.cpp.s"
	/Applications/Xcode.app/Contents/Developer/Toolchains/XcodeDefault.xctoolchain/usr/bin/c++ $(CXX_DEFINES) $(CXX_INCLUDES) $(CXX_FLAGS) -S /Users/raghavprasad/Work/BITS/Thesis/fictrac/exec/fictrac.cpp -o CMakeFiles/fictrac.dir/exec/fictrac.cpp.s

# Object files for target fictrac
fictrac_OBJECTS = \
"CMakeFiles/fictrac.dir/exec/fictrac.cpp.o"

# External object files for target fictrac
fictrac_EXTERNAL_OBJECTS =

../bin/fictrac: CMakeFiles/fictrac.dir/exec/fictrac.cpp.o
../bin/fictrac: CMakeFiles/fictrac.dir/build.make
../bin/fictrac: ../lib/libfictrac_core.a
../bin/fictrac: /Users/raghavprasad/vcpkg/installed/x64-osx/debug/lib/libopencv_dnnd.a
../bin/fictrac: /Users/raghavprasad/vcpkg/installed/x64-osx/lib/libprotobuf.a
../bin/fictrac: /Users/raghavprasad/vcpkg/installed/x64-osx/debug/lib/libopencv_highguid.a
../bin/fictrac: /Users/raghavprasad/vcpkg/installed/x64-osx/debug/lib/libopencv_mld.a
../bin/fictrac: /Users/raghavprasad/vcpkg/installed/x64-osx/debug/lib/libopencv_objdetectd.a
../bin/fictrac: /Users/raghavprasad/vcpkg/installed/x64-osx/debug/lib/libquircd.a
../bin/fictrac: /Users/raghavprasad/vcpkg/installed/x64-osx/debug/lib/libopencv_photod.a
../bin/fictrac: /Users/raghavprasad/vcpkg/installed/x64-osx/debug/lib/libopencv_stitchingd.a
../bin/fictrac: /Users/raghavprasad/vcpkg/installed/x64-osx/debug/lib/libopencv_videod.a
../bin/fictrac: /Users/raghavprasad/vcpkg/installed/x64-osx/debug/lib/libopencv_calib3dd.a
../bin/fictrac: /Users/raghavprasad/vcpkg/installed/x64-osx/debug/lib/libopencv_features2dd.a
../bin/fictrac: /Users/raghavprasad/vcpkg/installed/x64-osx/debug/lib/libopencv_flannd.a
../bin/fictrac: /Users/raghavprasad/vcpkg/installed/x64-osx/debug/lib/libopencv_videoiod.a
../bin/fictrac: /Users/raghavprasad/vcpkg/installed/x64-osx/debug/lib/libopencv_imgcodecsd.a
../bin/fictrac: /Users/raghavprasad/vcpkg/installed/x64-osx/lib/libjpeg.a
../bin/fictrac: /Users/raghavprasad/vcpkg/installed/x64-osx/lib/libwebp.a
../bin/fictrac: /Users/raghavprasad/vcpkg/installed/x64-osx/lib/libwebpdecoder.a
../bin/fictrac: /Users/raghavprasad/vcpkg/installed/x64-osx/lib/libwebpdemux.a
../bin/fictrac: /Users/raghavprasad/vcpkg/installed/x64-osx/lib/libpng.a
../bin/fictrac: /Users/raghavprasad/vcpkg/installed/x64-osx/lib/libtiff.a
../bin/fictrac: /Users/raghavprasad/vcpkg/installed/x64-osx/lib/liblzma.a
../bin/fictrac: /Users/raghavprasad/vcpkg/installed/x64-osx/lib/libjpeg.a
../bin/fictrac: /Users/raghavprasad/vcpkg/installed/x64-osx/lib/libwebp.a
../bin/fictrac: /Users/raghavprasad/vcpkg/installed/x64-osx/lib/libwebpdecoder.a
../bin/fictrac: /Users/raghavprasad/vcpkg/installed/x64-osx/lib/libwebpdemux.a
../bin/fictrac: /Users/raghavprasad/vcpkg/installed/x64-osx/lib/libpng.a
../bin/fictrac: /Users/raghavprasad/vcpkg/installed/x64-osx/lib/libtiff.a
../bin/fictrac: /Users/raghavprasad/vcpkg/installed/x64-osx/lib/liblzma.a
../bin/fictrac: /Users/raghavprasad/vcpkg/installed/x64-osx/debug/lib/libopencv_imgprocd.a
../bin/fictrac: /Users/raghavprasad/vcpkg/installed/x64-osx/debug/lib/libopencv_cored.a
../bin/fictrac: /Users/raghavprasad/vcpkg/installed/x64-osx/lib/libz.a
../bin/fictrac: /Users/raghavprasad/vcpkg/installed/x64-osx/debug/lib/libnlopt.a
../bin/fictrac: CMakeFiles/fictrac.dir/link.txt
	@$(CMAKE_COMMAND) -E cmake_echo_color --switch=$(COLOR) --green --bold --progress-dir=/Users/raghavprasad/Work/BITS/Thesis/fictrac/build/CMakeFiles --progress-num=$(CMAKE_PROGRESS_2) "Linking CXX executable ../bin/fictrac"
	$(CMAKE_COMMAND) -E cmake_link_script CMakeFiles/fictrac.dir/link.txt --verbose=$(VERBOSE)

# Rule to build all files generated by this target.
CMakeFiles/fictrac.dir/build: ../bin/fictrac

.PHONY : CMakeFiles/fictrac.dir/build

CMakeFiles/fictrac.dir/clean:
	$(CMAKE_COMMAND) -P CMakeFiles/fictrac.dir/cmake_clean.cmake
.PHONY : CMakeFiles/fictrac.dir/clean

CMakeFiles/fictrac.dir/depend:
	cd /Users/raghavprasad/Work/BITS/Thesis/fictrac/build && $(CMAKE_COMMAND) -E cmake_depends "Unix Makefiles" /Users/raghavprasad/Work/BITS/Thesis/fictrac /Users/raghavprasad/Work/BITS/Thesis/fictrac /Users/raghavprasad/Work/BITS/Thesis/fictrac/build /Users/raghavprasad/Work/BITS/Thesis/fictrac/build /Users/raghavprasad/Work/BITS/Thesis/fictrac/build/CMakeFiles/fictrac.dir/DependInfo.cmake --color=$(COLOR)
.PHONY : CMakeFiles/fictrac.dir/depend
