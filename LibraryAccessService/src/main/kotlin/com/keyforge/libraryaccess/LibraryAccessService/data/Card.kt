package com.keyforge.libraryaccess.LibraryAccessService.data

import javax.persistence.*

@Entity
data class Card (
    @Id
    val id: Int = 0,
    val name: String = "",
    @OneToOne(fetch = FetchType.LAZY)
    @JoinColumn(name = "typeId")
    val type: Type,
    val text: String = "",
    val aember: String? = null,
    val power: String? = null,
    val armor: String? = null,
    @OneToOne(fetch = FetchType.LAZY)
    @JoinColumn(name = "rarityId")
    val rarityId: Rarity,
    val artist: String = ""
)
