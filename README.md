# SentencePieceSharp
a  wrapper for SentencePiece project

#### ���ʹ��
##### 1.����ʹ��LibSentencePiece
windows��ʹ��cmake����LibSentencePiece
```bash
cd /path/to/LibSentencePiece
mkdir build; cd build
cmake .. -G "Visual Studio 16 2019" -A x64
// cmake .. -G "Visual Studio 17 2022" -A x64
cmake --build . --config Release
```
Linux��ʹ��cmake����LibSentencePiece
```bash
cd /path/to/LibSentencePiece
mkdir build && cd build
cmake .. -G "Unix Makefiles"
cmake --build . --config Release
```
##### 2.��/path/to/LibSentencePiece/build/Release/LibSentencePiece.dll��LibSentencePiece.lib������SentencePieceSharp��ĿĿ¼��
##### 3.��SentencePieceSharp��Ŀ������LibSentencePiece.dll
##### 4.��SentencePieceSharp.Examples�������Ŀ����SentencePieceSharp
##### 5.����SentencePieceSharp.Examples��Ŀ���в���

#### ��������
##### 1.��[K2TransducerAsr](https://github.com/manyeyes/K2TransducerAsr)�У�ʹ��sherpa-onnx-streaming-zipformer-small-ctc-zh-int8-2025-04-01ģ��ʱ����Ҫͨ��bbpe.model����ʱʹ��SentencePieceSharp��
##### 2.������c#��Ŀ����Ҫʹ��SentencePiece�ĵط�������ʹ��SentencePieceSharp��


