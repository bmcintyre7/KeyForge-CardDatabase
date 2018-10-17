package com.keyforge.libraryaccess.LibraryAccessService.data

import com.keyforge.libraryaccess.LibraryAccessService.responses.RarityBody
import java.security.Key
import javax.persistence.*

@Entity
@Table(name = "card")
data class Card (
    @Id
    @GeneratedValue(strategy=GenerationType.IDENTITY)
    val id: Int? = null,
    val name: String = "",
    @OneToOne(fetch = FetchType.LAZY, cascade = [CascadeType.PERSIST])
    @JoinColumn(name = "typeId")
    val type: Type,
    val text: String = "",
    val aember: String? = null,
    val power: String? = null,
    val armor: String? = null,
    @OneToOne(fetch = FetchType.LAZY, cascade = [CascadeType.PERSIST])
    @JoinColumn(name = "rarityId")
    val rarity: Rarity,
    @OneToMany(fetch = FetchType.LAZY, mappedBy = "card")
    val expansions: List<CardExpansions>,
    @OneToMany(fetch = FetchType.LAZY, mappedBy = "card", targetEntity = CardHouses::class)
    val houses: List<House>,
    @OneToMany(fetch = FetchType.LAZY, mappedBy = "card", targetEntity = CardTraits::class)
    val traits: List<Trait>,
    @OneToMany(fetch = FetchType.LAZY, mappedBy = "card", targetEntity = CardKeywords::class)
    val keywords: List<Keyword>,
    val artist: String = ""

)
