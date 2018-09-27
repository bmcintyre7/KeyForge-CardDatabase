package com.keyforge.libraryaccess.LibraryAccessService.data

import javax.persistence.*

@Entity
@Table(name = "rarity")
data class Rarity (
    @Id
    @GeneratedValue(strategy= GenerationType.IDENTITY)
    val id: Int? = null,
    val name: String = ""
)