package com.keyforge.libraryaccess.LibraryAccessService.data

import javax.persistence.*

@Entity
@Table(name = "trait")
data class Trait (
    @Id
    @GeneratedValue(strategy= GenerationType.IDENTITY)
    val id: Int? = null,
    val name: String = ""
)