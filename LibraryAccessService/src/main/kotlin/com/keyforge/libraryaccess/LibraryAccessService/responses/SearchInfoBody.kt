package com.keyforge.libraryaccess.LibraryAccessService.responses

data class SearchInfoBody (
    val houses: List<String>,
    val traits: List<String>,
    val keywords: List<String>,
    val types: List<String>
)