package com.keyforge.libraryaccess.LibraryAccessService.data

import javax.persistence.*

@Entity
@Table(name = "expansion")
data class Expansion (
    @Id
    @GeneratedValue(strategy= GenerationType.IDENTITY)
    val id: Int? = null,
    val name: String = ""
)