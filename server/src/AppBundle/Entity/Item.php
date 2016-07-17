<?php
// src/Appbundle/Entity/Item.php
namespace AppBundle\Entity;

use Doctrine\ORM\Mapping as ORM;

/**
* @ORM\Entity
* @ORM\Table(name="item")
*/
class Item
{
    /**
    * @ORM\Column(type="integer")
     * @ORM\Id
     * @ORM\GeneratedValue(strategy="AUTO")
     */
    private $id;

    /**
     * @ORM\Column(type="string", length=100)
     */
    private $name;

    /**
     * @ORM\Column(type="string", length=256)
     */
    private $description;

    /**
     * @ORM\Column(type="float")
     */
    private $type;

    /**
     * @ORM\Column(type="float")
     */
    private $rarity;

    /**
     * @ORM\ManyToMany(targetEntity="Avatar", inversedBy="items")
     */
     private $avatars;

    /**
     * Get id
     *
     * @return integer
     */
    public function getId()
    {
        return $this->id;
    }

    /**
     * Set name
     *
     * @param string $name
     *
     * @return Item
     */
    public function setName($name)
    {
        $this->name = $name;

        return $this;
    }

    /**
     * Get name
     *
     * @return string
     */
    public function getName()
    {
        return $this->name;
    }
    /**
     * Constructor
     */
    public function __construct()
    {
        $this->avatars = new \Doctrine\Common\Collections\ArrayCollection();
    }

    /**
     * Set description
     *
     * @param string $description
     *
     * @return Item
     */
    public function setDescription($description)
    {
        $this->description = $description;

        return $this;
    }

    /**
     * Get description
     *
     * @return string
     */
    public function getDescription()
    {
        return $this->description;
    }

    /**
     * Set type
     *
     * @param  $type
     *
     * @return Item
     */
    public function setType($type)
    {
        $this->type = $type;

        return $this;
    }

    /**
     * Get type
     *
     * @return
     */
    public function getType()
    {
        return $this->type;
    }

    /**
     * Set rarity
     *
     * @param \float $rarity
     *
     * @return Item
     */
    public function setRarity( $rarity)
    {
        $this->rarity = $rarity;

        return $this;
    }

    /**
     * Get rarity
     *
     * @return
     */
    public function getRarity()
    {
        return $this->rarity;
    }

    /**
     * Add avatars
     *
     * @param \AppBundle\Entity\Avatar $avatars
     *
     * @return Item
     */
    public function addAvatar(\AppBundle\Entity\Avatar $avatars)
    {
        $this->avatars[] = $avatars;

        return $this;
    }

    /**
     * Remove avatars
     *
     * @param \AppBundle\Entity\Avatar $avatars
     */
    public function removeAvatar(\AppBundle\Entity\Avatar $avatars)
    {
        $this->avatars->removeElement($avatars);
    }

    /**
     * Get avatar
     *
     * @return \Doctrine\Common\Collections\Collection
     */
    public function getAvatar()
    {
        return $this->avatar;
    }

    public function __toString(){
      return $this->id;
    }
}
