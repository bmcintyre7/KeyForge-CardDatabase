package com.keyforge.libraryaccess.LibraryAccessService.data

import javax.persistence.*

@Entity
@Table(name = "card")
data class Card (
    @Id
    @GeneratedValue(strategy=GenerationType.IDENTITY)
    val id: Int? = null,
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
    val rarity: Rarity,
    val artist: String = ""
)
