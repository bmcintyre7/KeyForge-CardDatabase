package com.keyforge.libraryaccess.LibraryAccessService.data

import javax.persistence.*

@Entity
data class CardHouses (
    @Id
    val id: Int = 0,
    @OneToOne(fetch = FetchType.LAZY)
    @JoinColumn(name = "cardId")
    val card: Card,
    @OneToOne(fetch = FetchType.LAZY)
    @JoinColumn(name = "houseId")
    val house: House
)