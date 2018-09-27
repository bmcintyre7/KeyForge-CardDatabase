package com.keyforge.libraryaccess.LibraryAccessService.data

import javax.persistence.*

@Entity
@Table(name = "house")
data class House (
    @Id
    @GeneratedValue(strategy= GenerationType.IDENTITY)
    val id: Int? = null,
    val name: String = ""
)