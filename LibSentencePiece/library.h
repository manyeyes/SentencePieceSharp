#pragma once
#include "macros.h"

extern "C" EXPORT void* spp_create();

extern "C" EXPORT int spp_load(void* processor, const char* model_file);

extern "C" EXPORT int spp_encode(void* processor, const char* input, int** output);

extern "C" EXPORT void spp_free_array(const int* arr);

extern "C" EXPORT void spp_destroy(void* processor);

extern "C" EXPORT int spp_get_piece_size(void* processor);

extern "C" EXPORT int spp_piece_to_id(void* processor, const char* piece);

extern "C" EXPORT const char* spp_id_to_piece(void *processor, int piece_id);

extern "C" EXPORT bool spp_is_unknown(void* processor, int piece);

extern "C" EXPORT bool spp_is_control(void* processor, int piece);

extern "C" EXPORT int spp_eos_id(void* processor);

extern "C" EXPORT int spp_bos_id(void* processor);

extern "C" EXPORT int spp_pad_id(void* processor);

extern "C" EXPORT int spp_unk_id(void* processor);

extern "C" EXPORT void spp_set_encode_extra_options(void* processor, const char *piece);

// Add decoding method

extern "C" EXPORT int spp_decode(void* processor, const int* input, int input_length, char** output);

extern "C" EXPORT void spp_free_string(char* str);

extern "C" EXPORT int spp_decode_pieces(void* processor, const char** input, int input_length, char** output);