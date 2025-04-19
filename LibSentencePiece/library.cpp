// The following encoding methods come from:
// https://github.com/Foorcee/libSentencePiece
// newly add decode and decode_pieces methods
#include "library.h"

#include "sentencepiece_processor.h"
#include <vector>
#include <cstring>
#include <iostream>

#include "sentencepiece_trainer.h"

void* spp_create() {
    return new sentencepiece::SentencePieceProcessor();
}

int spp_load(void* processor, const char* model_file) {
    const auto sp = static_cast<sentencepiece::SentencePieceProcessor*>(processor);
    if (const auto status = sp->Load(model_file); status.ok()) {
        return 0;
    }
    return 1;
}

int spp_encode(void* processor, const char* input, int** output) {
    const auto sp = static_cast<sentencepiece::SentencePieceProcessor*>(processor);
    std::vector<int> ids;

    if (const auto status = sp->Encode(input, &ids); !status.ok()) {
        return -1;
    }
    const int length = ids.size();
    *output = new int[length];
    std::memcpy(*output, ids.data(), length * sizeof(int));
    return length;
}

void spp_free_array(const int* arr) {
    delete[] arr;
}

void spp_destroy(void* processor) {
    const auto* sp = static_cast<sentencepiece::SentencePieceProcessor*>(processor);
    delete sp;
}

int spp_get_piece_size(void* processor) {
    const auto sp = static_cast<sentencepiece::SentencePieceProcessor*>(processor);
    return sp->GetPieceSize();
}

int spp_piece_to_id(void* processor, const char *piece) {
    const auto sp = static_cast<sentencepiece::SentencePieceProcessor*>(processor);
    return sp->PieceToId(piece);
}

const char* spp_id_to_piece(void *processor, int piece_id) {
    static std::string last_piece;
    const auto sp = static_cast<sentencepiece::SentencePieceProcessor*>(processor);
    last_piece = sp->IdToPiece(piece_id);
    return last_piece.c_str();
}

bool spp_is_unknown(void *processor, const int piece) {
    const auto sp = static_cast<sentencepiece::SentencePieceProcessor*>(processor);
    return sp->IsUnknown(piece);
}

bool spp_is_control(void *processor, const int piece) {
    const auto sp = static_cast<sentencepiece::SentencePieceProcessor*>(processor);
    return sp->IsControl(piece);
}

int spp_bos_id(void *processor) {
    const auto sp = static_cast<sentencepiece::SentencePieceProcessor*>(processor);
    return sp->bos_id();
}

int spp_eos_id(void *processor) {
    const auto sp = static_cast<sentencepiece::SentencePieceProcessor*>(processor);
    return sp->eos_id();
}

int spp_pad_id(void *processor) {
    const auto sp = static_cast<sentencepiece::SentencePieceProcessor*>(processor);
    return sp->pad_id();
}

int spp_unk_id(void *processor) {
    const auto sp = static_cast<sentencepiece::SentencePieceProcessor*>(processor);
    return sp->unk_id();
}

void spp_set_encode_extra_options(void* processor, const char *piece) {
    const auto sp = static_cast<sentencepiece::SentencePieceProcessor*>(processor);
    sp->SetEncodeExtraOptions(piece);
}

// �ͷŽ������ַ������ڴ�
void spp_free_string(char* str) {
    delete[] str;
}

// ���뺯�����������������Ϊ�ַ���
int spp_decode(void* processor, const int* input, int input_length, char** output) {
    const auto sp = static_cast<sentencepiece::SentencePieceProcessor*>(processor);
    std::vector<int> ids(input, input + input_length);
    std::string decoded;

    if (const auto status = sp->Decode(ids, &decoded); !status.ok()) {
        return -1;
    }

    const int length = decoded.length();
    *output = new char[length + 1];
    std::strcpy(*output, decoded.c_str());
    return length;
}

// ���뺯�������ַ����������Ϊ�ַ���
int spp_decode_pieces(void* processor, const char** input, int input_length, char** output) {
    const auto sp = static_cast<sentencepiece::SentencePieceProcessor*>(processor);
    std::vector<std::string> pieces;
    for (int i = 0; i < input_length; ++i) {
        pieces.emplace_back(input[i]);
    }
    std::string decoded;

    if (const auto status = sp->Decode(pieces, &decoded); !status.ok()) {
        return -1;
    }

    const int length = decoded.length();
    *output = new char[length + 1];
    std::strcpy(*output, decoded.c_str());
    return length;
}

// ���벢���� SentencePieceText ���ͽ��������򻯴������������л�����ַ�����
//int spp_decode_to_spt(void* processor, const int* input, int input_length, char** output) {
//    const auto sp = static_cast<sentencepiece::SentencePieceProcessor*>(processor);
//    std::vector<int> ids(input, input + input_length);
//    sentencepiece::SentencePieceText spt;
//
//    if (const auto status = sp->Decode(ids, &spt); !status.ok()) {
//        return -1;
//    }
//
//    std::string serialized = spt.SerializeAsString();
//    const int length = serialized.length();
//    *output = new char[length + 1];
//    std::strcpy(*output, serialized.c_str());
//    return length;
//}