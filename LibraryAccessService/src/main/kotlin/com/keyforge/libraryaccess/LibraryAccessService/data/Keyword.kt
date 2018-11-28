package com.keyforge.libraryaccess.LibraryAccessService.data

import javax.persistence.*

@Entity
@Table(name = "keyword")
data class Keyword (
    @Id
    @GeneratedValue(strategy= GenerationType.IDENTITY)
    val id: Int? = null,
    val name: String = ""
) {
    fun toSearchInfoString() = name
}