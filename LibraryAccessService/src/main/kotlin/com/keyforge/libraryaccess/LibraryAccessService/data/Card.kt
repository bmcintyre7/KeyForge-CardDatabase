package com.keyforge.libraryaccess.LibraryAccessService.data

import com.fasterxml.jackson.annotation.JsonIgnore
import com.keyforge.libraryaccess.LibraryAccessService.responses.*
import org.hibernate.annotations.BatchSize
import org.hibernate.annotations.Fetch
import org.hibernate.annotations.FetchMode
import java.text.Normalizer
import java.util.stream.Collectors
import javax.persistence.*

@Entity
@Cacheable(value = true)
@BatchSize(size=500)
@Table(name = "card")
data class Card (
        @Id
    @GeneratedValue(strategy=GenerationType.IDENTITY)

    val id: Int? = null,
    val name: String = "",
    @OneToOne(fetch = FetchType.LAZY, cascade = [CascadeType.PERSIST])
    @Fetch(FetchMode.JOIN)
    @JoinColumn(name = "typeId")
    val type: Type,

    val text: String = "",
    val aember: String? = null,
    val power: String? = null,
    val armor: String? = null,

    @OneToOne(fetch = FetchType.LAZY, cascade = [CascadeType.PERSIST])
    @Fetch(FetchMode.JOIN)
    @JoinColumn(name = "rarityId")
    val rarity: Rarity,

    @OneToMany(fetch = FetchType.LAZY, mappedBy = "card")
    @BatchSize(size=500)
    val expansions: List<CardExpansions>,

    @OneToMany(fetch = FetchType.LAZY)//, targetEntity = CardHouses::class)
    @JoinTable(
            name = "cardHouses",
            joinColumns = [JoinColumn(name = "cardId")],
            inverseJoinColumns = [JoinColumn(name = "houseId")]
    )
    @BatchSize(size=500)
    val houses: List<House>,

    @OneToMany(fetch = FetchType.LAZY, mappedBy = "card")//, targetEntity = CardTraits::class)
//    @JoinTable(
//            name = "cardTraits",
//            joinColumns = [JoinColumn(name = "cardId")],
//            inverseJoinColumns = [JoinColumn(name = "traitId")]
//    )
    @BatchSize(size=500)
    val traits: List<CardTraits>,

    @OneToMany(fetch = FetchType.LAZY, mappedBy = "card")//, targetEntity = CardKeywords::class)
    @BatchSize(size=500)
    @JsonIgnore
    val keywords: List<CardKeywords>,
    val artist: String = ""
) {
    fun toDetailedCardBody() = DetailedCardBody(
        name = name,
        type = type.name,
        text = text,
        aember = aember,
        power = power,
        armor = armor,
        rarity = rarity.name,
        expansions = expansions.stream().map { cardExp -> cardExp.expansion.abbreviation + " #" + cardExp.number }.collect(Collectors.toList()),
        imageNames = expansions.stream().map { cardExp -> cardExp.expansion.abbreviation.toLowerCase() + "-" + cardExp.number }.collect(Collectors.toList()),
        houses = houses.stream().map { house -> house.name }.collect(Collectors.toList()),
        traits = traits.stream().map { trait -> trait.trait.name }.collect(Collectors.toList()),
        keywords = keywords.stream().map { keyword -> keyword.keyword.name }.collect(Collectors.toList()),
        artist = artist
    )
    fun toCardBody() = CardBody(
        name = name,
        expansions = expansions.stream().map { cardExp -> ExpansionBody(cardExp.expansion.name, cardExp.expansion.abbreviation, cardExp.number, cardExp.expansion.abbreviation.toLowerCase() + "-" + cardExp.number) }.collect(Collectors.toList()),
        imageNames = expansions.stream().map { cardExp -> cardExp.expansion.abbreviation.toLowerCase() + "-" + cardExp.number }.collect(Collectors.toList())
    )
    fun toDiscordCardBody() = DiscordCardBody(
        name = name,
        type = type.name,
        text = text,
        aember = aember,
        power = power,
        armor = armor,
        rarity = rarity.name,
        directLink = "http://libraryaccess.net/cards/" + expansions[0].expansion.abbreviation.toUpperCase() + "/" + expansions[0].number,
        imageLink = "http://libraryaccess.net/images/cards/" + expansions[0].expansion.abbreviation.toLowerCase() + "-" + expansions[0].number + ".jpg",
        expansions = expansions.stream().map { cardExp -> cardExp.expansion.abbreviation + " #" + cardExp.number }.collect(Collectors.toList()),
        houses = houses.stream().map { house -> house.name }.collect(Collectors.toList()),
        traits = traits.stream().map { trait -> trait.trait.name }.collect(Collectors.toList()),
        keywords = keywords.stream().map { keyword -> keyword.keyword.name }.collect(Collectors.toList()),
        artist = artist
    )

    fun slugify(word: String, replacement: String = "-") = Normalizer
        .normalize(word, Normalizer.Form.NFD)
        .replace("[^\\p{ASCII}]".toRegex(), "")
        .replace("[^a-zA-Z0-9\\s]+".toRegex(), "").trim()
        .replace("\\s+".toRegex(), replacement)
        .toLowerCase()
}
