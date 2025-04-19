# SentencePieceSharp
a  wrapper for SentencePiece project

#### 如何使用
1.编译和打包LibSentencePiece
windows下使用cmake编译LibSentencePiece
```bash
cd /path/to/LibSentencePiece
mkdir build; cd build
cmake .. -G "Visual Studio 16 2019" -A x64
// cmake .. -G "Visual Studio 17 2022" -A x64
cmake --build . --config Release
```
Linux下使用cmake编译LibSentencePiece
```bash
cd /path/to/LibSentencePiece
mkdir build && cd build
cmake .. -G "Unix Makefiles"
cmake --build . --config Release
```
2.将/path/to/LibSentencePiece/build/Release/LibSentencePiece.dll和LibSentencePiece.lib拷贝到SentencePieceSharp项目目录下
3.在SentencePieceSharp项目中引用LibSentencePiece.dll
4.在SentencePieceSharp.Examples中添加项目引用SentencePieceSharp
5.启动SentencePieceSharp.Examples项目进行测试

#### 在哪里用
1.在[K2TransducerAsr](https://github.com/manyeyes/K2TransducerAsr)中，使用sherpa-onnx-streaming-zipformer-small-ctc-zh-int8-2025-04-01模型时，需要通过bbpe.model解码时使用SentencePieceSharp。
2.其他（c#项目）需要使用SentencePiece的地方都可以使用SentencePieceSharp。


