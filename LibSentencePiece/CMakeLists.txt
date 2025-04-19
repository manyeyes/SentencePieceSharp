include(FetchContent)

cmake_minimum_required(VERSION 3.5)
project(SentencePiece LANGUAGES CXX)

set(CMAKE_CXX_STANDARD 17)
set(BUILD_SHARED_LIBS ON)
set(CMAKE_CXX_STANDARD_REQUIRED ON)

add_definitions(-DBUILD_DLL)

FetchContent_Declare(
        sentencepiece
        # 网络正常时，使用git仓库，自动下载
        GIT_REPOSITORY https://github.com/google/sentencepiece.git
        GIT_TAG v0.2.0
        # 下载失败时，指定本地路径
        # 手动下载 https://github.com/google/sentencepiece.git 源码，存放于 /build/_deps/sentencepiece-master 中
        # SOURCE_DIR ./_deps/sentencepiece-master
)

FetchContent_MakeAvailable(sentencepiece)


add_library(SentencePiece SHARED library.h library.cpp
        macros.h
)

if (WIN32)
    set(CMAKE_SHARED_LIBRARY_PREFIX "lib")
endif()

# Include the SentencePiece headers
target_include_directories(SentencePiece PRIVATE
        ${sentencepiece_SOURCE_DIR}/src
        ${sentencepiece_BINARY_DIR}
)

# Link against SentencePiece
target_link_libraries(SentencePiece LINK_PRIVATE sentencepiece)